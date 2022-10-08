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

        private int _currentPlayerIndex = 0;
        
        private void OnEnable()
        {
            CountdownTimer.OnCountedDown += NextTurn;
            PlayerManager.OnCurrentPlayerChanged += RefreshPlayerIndex;
        }

        private void OnDisable()
        {
            CountdownTimer.OnCountedDown -= NextTurn;
            PlayerManager.OnCurrentPlayerChanged -= RefreshPlayerIndex;
        }

        private void RefreshPlayerIndex(Player player)
        {
            _currentPlayerIndex = PlayerManager.Instance.IdToIndex(player.id);
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
            int nextIndex;
            if (PlayerManager.Instance.IdToIndex(PlayerManager.Instance.CurrentPlayer.id) == -1) // current player died
            {
                nextIndex = _currentPlayerIndex % PlayerManager.Instance.NumPlayers;
            }
            else
            {
                nextIndex = (_currentPlayerIndex + 1) % PlayerManager.Instance.NumPlayers;
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


