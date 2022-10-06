using System.Collections;
using Audio;
using Camera;
using Items;
using Items.Hats;
using Oscillators;
using Player.Physics_Based_Character_Controller;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerSelection : MonoBehaviour
    {
        // Constants
        private const float DEFAULT_RIDE_HEIGHT = 2f;
        private const float UPPER_RIDE_HEIGHT = 2.75f;

        private const float DEFAULT_PLAYER_SIZE = 1f;
        private const float MAX_PLAYER_SIZE = 1.2f;
    
        // Variables
        private global::Player.Player _previousPlayer;

        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private ItemRack _hatRack;

        [SerializeField] private Button _finaliseSelectionButton;

        private void OnEnable()
        {
            PlayerManager.Instance.OnCurrentPlayerChanged += ChangeCurrentPlayerSelection;
            LoadingScreen.Instance.OnTransitionedToLoadingScreen += BehindTheCurtain;
        }

        private void OnDisable()
        {
            PlayerManager.Instance.OnCurrentPlayerChanged -= ChangeCurrentPlayerSelection;
            LoadingScreen.Instance.OnTransitionedToLoadingScreen -= BehindTheCurtain;
        }

        private void Start()
        {
            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
            _previousPlayer = currentPlayer;
            // Update name placeholder text
            ((TextMeshProUGUI)_nameInput.placeholder).text = currentPlayer.suggestedName;
        }
    
        private void ChangeCurrentPlayerSelection(global::Player.Player player)
        {
            AdjustScales(player);
            AdjustRideHeights(player);
            AdjustMaterials(player);

            _previousPlayer = player;
        }

        private void AdjustScales(global::Player.Player player)
        {
            AdjustScale(_previousPlayer, false);
            AdjustScale(player, true);
        }

        private void AdjustScale(global::Player.Player player, bool shouldEnlarge)
        {
            StartCoroutine(EasedLerpScale(player.transform, shouldEnlarge));
        }

        private void AdjustRideHeights(global::Player.Player player)
        {
            _previousPlayer.AdjustRideHeight(DEFAULT_RIDE_HEIGHT);
            player.AdjustRideHeight(UPPER_RIDE_HEIGHT);
        }

        private void AdjustMaterials(global::Player.Player player)
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
                CinemachineShake.Instance.ShakeCamera(2.5f, 0.4f);
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
                CinemachineShake.Instance.ShakeCamera(2.5f, 0.4f);
            }
        }

        public void FinaliseSelection()
        {
            AudioManager.Instance.Play("Click Secondary");

            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
            AdjustScale(currentPlayer, false);

            LoadingScreen.Instance.TransitionToLoadingScreen();
        }

        private void BehindTheCurtain()
        {
            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
        
            // Save Edits to final Player
            currentPlayer.EditPlayerSettings(_nameInput.text, _hatRack.CurrentItem);
        
            PlayerManager.Instance.FinaliseNumberOfPlayers(currentPlayer.id + 1);

            // For each remaining player...
            foreach (global::Player.Player player in PlayerManager.Instance.Players)
            {
                player.AdjustRideHeight(DEFAULT_RIDE_HEIGHT);
                player.SetLookDirectionOption(PhysicsBasedCharacterController.LookDirectionOptions.Velocity);
                player.RestoreDefaultMaterials();
                player.EnableParticleSystem();

                // Save their settings
                player.PackPlayerSettings();
            }

            LoadingScreen.Instance.TransitionFromLoadingScreen(SceneIndices.GAME);
        }
    
        private IEnumerator EasedLerpScale(Transform transform, bool shouldEnlarge)
        {
            var t = 0f;
            while (Mathf.Abs(t) < 1f)
            {
                var easedT = Easing.Back.Out(t);
                var size = (shouldEnlarge ? DEFAULT_PLAYER_SIZE : MAX_PLAYER_SIZE) + easedT * (MAX_PLAYER_SIZE - DEFAULT_PLAYER_SIZE) * (shouldEnlarge ? 1 : -1);
                //transform.localScale = Vector3.one * size;
                transform.GetComponent<SquashAndStretch>().LocalEquilibriumScale = Vector3.one * size;
                yield return new WaitForFixedUpdate(); // Surely this is poor practise.
                t += Time.deltaTime; // Is this the correct deltaTime for a IEnumerator?
            }
            yield break;
        }
    }
}
