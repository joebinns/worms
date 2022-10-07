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
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private ItemRack _hatRack;

        [SerializeField] private Button _finaliseSelectionButton;

        private void Start()
        {
            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
            // Update name placeholder text
            ((TextMeshProUGUI)_nameInput.placeholder).text = currentPlayer.suggestedName;
        }

        public void NextPlayer()
        {
            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
            if (currentPlayer.id < 3)
            {
                // Activate players hat
                currentPlayer.HatSlot.gameObject.SetActive(true);

                // Change to next Player
                ChangePlayer(currentPlayer.id + 1);
            }
            else
            {
                CinemachineShake.Instance.InvalidInputPresetShake();
                AudioManager.Instance.Play("Error");
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
            
            // Enable / Disable Let's Go! button
            _finaliseSelectionButton.gameObject.SetActive(id != 0);

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
                // TODO: For some reason, currentPlayer.Hat.gameObject.SetActive() isn't working, so I've made HatSlot public and used it instead.
                currentPlayer.HatSlot.gameObject.SetActive(false); 

                // Change to previous player
                currentPlayer = ChangePlayer(currentPlayer.id - 1);

                // Deactivate previous players hat
                currentPlayer.HatSlot.gameObject.SetActive(false);
            }
            else
            {
                CinemachineShake.Instance.InvalidInputPresetShake();
                AudioManager.Instance.Play("Error");
            }
        }
        
        public void Back()
        {
            AudioManager.Instance.Play("Click Secondary");
            LoadingScreen.Instance.ChangeSceneImpatient(SceneIndices.MAIN_MENU);
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
    }
}
