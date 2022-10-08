using Cameras;
using Items.Weapons;
using Players;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class TurnManager : MonoBehaviour // make me a singleton...
    {
        #region Singleton
        public static TurnManager Instance;
        #endregion
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnEnable()
        {
            CountdownTimer.OnCountedDown += NextTurn;
        }

        private void OnDisable()
        {
            CountdownTimer.OnCountedDown -= NextTurn;
        }

        private void ChangeTurn(int playerIndex)
        {
            CameraManager.Instance.UpdateCameraState(CameraState.FollowCamera);
            var newPlayer = PlayerManager.Instance.SetCurrentPlayer(playerIndex);
            // Reset new player's ammo
            foreach (GameObject item in newPlayer.WeaponRack.Items)
            {
                var weapon = item.GetComponent<Weapon>();
                if (weapon != null)
                {
                    weapon.ResetAmmunition();
                }
            }
        }

        public void NextTurn()
        {
            if (PlayerManager.Instance.SelectedNumPlayers == 0) { return; }

            var nextPlayerId = (PlayerManager.Instance.CurrentPlayer.id) % PlayerManager.Instance.SelectedNumPlayers;
            var nextIndex = -1;
            while (nextIndex == -1)
            {
                nextPlayerId = (nextPlayerId + 1) % PlayerManager.Instance.SelectedNumPlayers;
                nextIndex = PlayerManager.Instance.IdToIndex(nextPlayerId);
            }
            
            ChangeTurn(nextIndex);
        }
    
        public void NextTurnInputAction(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            NextTurn();
        }
    }
}


