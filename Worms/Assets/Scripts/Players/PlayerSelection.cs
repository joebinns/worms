using System.Collections;
using Audio;
using Cameras;
using Items;
using Items.Hats;
using Oscillators;
using Players.Physics_Based_Character_Controller;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Players
{
    public class PlayerSelection : MonoBehaviour
    {
        // Constants
        private const float DEFAULT_RIDE_HEIGHT = 2f;
        private const float UPPER_RIDE_HEIGHT = 2.75f;

        private const float DEFAULT_PLAYER_SIZE = 1f;
        private const float MAX_PLAYER_SIZE = 1.2f;
    
        // Variables
        private Player _previousPlayer;

        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private ItemRack _hatRack;

        [SerializeField] private Button _finaliseSelectionButton;

        private void OnEnable()
        {
            PlayerManager.Instance.OnCurrentPlayerChanged += ChangeCurrentPlayerSelection;
        }

        private void OnDisable()
        {
            PlayerManager.Instance.OnCurrentPlayerChanged -= ChangeCurrentPlayerSelection;
        }

        private void Start()
        {
            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
            _previousPlayer = currentPlayer;
            // Update name placeholder text
            ((TextMeshProUGUI)_nameInput.placeholder).text = currentPlayer.suggestedName;
        }
    
        private void ChangeCurrentPlayerSelection(Player player)
        {
            AdjustScales(player);
            AdjustRideHeights(player);
            AdjustMaterials(player);

            _previousPlayer = player;
        }

        private void AdjustScales(Player player)
        {
            AdjustScale(_previousPlayer, false);
            AdjustScale(player, true);
        }

        private void AdjustScale(Player player, bool shouldEnlarge)
        {
            StartCoroutine(EasedLerpScale(player.transform, shouldEnlarge));
        }

        private void AdjustRideHeights(Player player)
        {
            _previousPlayer.AdjustRideHeight(DEFAULT_RIDE_HEIGHT);
            player.AdjustRideHeight(UPPER_RIDE_HEIGHT);
        }

        private void AdjustMaterials(Player player)
        {
            if (_previousPlayer.id > player.id) // If selection moves to fewer players...
            {
                _previousPlayer.EnableDitherMode();
            }
            player.RestoreDefaultMaterials();
        }
    
        public void NextPlayer()
        {
            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
            if (currentPlayer.id < 3)
            {
                // Activate players hat
                currentPlayer.Hat.gameObject.SetActive(true);

                // Change to next Player
                ChangePlayer(currentPlayer.id + 1);

            }
            else
            {
                CinemachineShake.Instance.InvalidInputPresetShake();
            }
        }

        private Player ChangePlayer(int id)
        {
            AudioManager.Instance.Play("Click Primary");

            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
        
            // Save edits to Player
            currentPlayer.EditPlayerSettings(_nameInput.text, _hatRack.CurrentItem);
        
            // Change to next Player
            currentPlayer = PlayerManager.Instance.SetCurrentPlayer(id);
            if (id == 0)
            {
                // Disable Let's Go! button
                _finaliseSelectionButton.gameObject.SetActive(false);
            }
            else
            {
                // Enable Let's Go! button
                _finaliseSelectionButton.gameObject.SetActive(true);
            }

            // Update name placeholder text
            ((TextMeshProUGUI)_nameInput.placeholder).text = currentPlayer.suggestedName;

            // Restore saved edits
            _nameInput.text = currentPlayer.name;
            _hatRack.ChangeItem(currentPlayer.Hat.GetComponent<Hat>().id);

            return currentPlayer;
        }

        public void PreviousPlayer()
        {
            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
            if (currentPlayer.id > 0)
            {
                // Deactivate latest players hat
                currentPlayer.Hat.gameObject.SetActive(false);

                // Change to previous player
                currentPlayer = ChangePlayer(currentPlayer.id - 1);

                // Deactivate previous players hat
                currentPlayer.Hat.gameObject.SetActive(false);

            }
            else
            {
                CinemachineShake.Instance.InvalidInputPresetShake();
            }
        }

        public void FinaliseSelection()
        {
            AudioManager.Instance.Play("Click Secondary");
            SaveFinalSelection();
            LoadingScreen.Instance.ChangeSceneImpatient(SceneIndices.GAME);
        }
        
        private void SaveFinalSelection()
        {
            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
            currentPlayer.EditPlayerSettings(_nameInput.text, _hatRack.CurrentItem); // Apply edits to final player
            PlayerManager.Instance.FinaliseNumberOfPlayers(currentPlayer.id + 1); // Set shouldSpawn = false on excess players scriptable objects
            // For each remaining player...
            foreach (Player player in PlayerManager.Instance.Players)
            {
                // Save their settings
                player.PackPlayerSettings();
            }
        }
    
        // I tried to seperate this function into it's own utilities file (see Easing.EasedLerp), as similar variations
        // in a few places. However, I can't see how I could  get the IEnumerator to return a (i.e. float size) on every
        // loop, which the caller would pick up on. 
        private IEnumerator EasedLerpScale(Transform transform, bool shouldEnlarge)
        {
            var t = 0f;
            while (Mathf.Abs(t) < 1f)
            {
                var easedT = EasingUtils.Back.Out(t);
                var size = (shouldEnlarge ? DEFAULT_PLAYER_SIZE : MAX_PLAYER_SIZE) + easedT * (MAX_PLAYER_SIZE - DEFAULT_PLAYER_SIZE) * (shouldEnlarge ? 1 : -1);
                transform.GetComponent<SquashAndStretch>().LocalEquilibriumScale = Vector3.one * size;
                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}
