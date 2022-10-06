using Audio;
using Cameras;
using Players;
using Players.Physics_Based_Character_Controller;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        #region Singleton
        public static InputManager Instance;
        #endregion
    
        private PlayerInput _playerInput;
        private InputActionMap _previousActionMap;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
            _playerInput = GetComponent<PlayerInput>();
            _previousActionMap = _playerInput.currentActionMap;
        }

        private void OnEnable()
        {
            CameraManager.Instance.OnCameraStateChanged += ChangeControlState;
        }

        private void OnDisable()
        {
            CameraManager.Instance.OnCameraStateChanged -= ChangeControlState;
        }

        private void ChangeControlState(CameraState state)
        {
            switch (state)
            {
                case CameraState.FollowCamera:
                    SwitchActionMap("Moving");
                    SwapWeapon(0);
                    break;
                case CameraState.AimCamera:
                    SwitchActionMap("Aiming");
                    break;
            }
        }

        public void PrimaryHotbarSlotInputAction(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
        
            SwapWeapon(1);
        }
    
        public void SecondaryHotbarSlotInputAction(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            SwapWeapon(2);
        }

        public void EmptyHotbarSlotInputAction(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            SwapWeapon(0);
        }

        public void SwapWeapon(int index)
        {
            // Change weapon rack item, change hotbar icon and update ammunition
            PlayerManager.Instance.CurrentPlayer.GetComponent<Players.Player>().WeaponRack.ChangeItem(index);
            UIManager.Instance.SwitchActiveHotbar(index);
            if (index == 0)
            {
                UIManager.Instance.HideAmmunition();
            }
            else
            {
                AudioManager.Instance.Play("Click Primary");
                UIManager.Instance.ShowAmmunition();
                UIManager.Instance.RefreshAmmunition();
            }
        }

        public void MoveInputAction(InputAction.CallbackContext context)
        {
            PlayerManager.Instance.CurrentPlayer.GetComponent<PhysicsBasedCharacterController>().MoveInputAction(context);

        }

        public void JumpInputAction(InputAction.CallbackContext context)
        {
            PlayerManager.Instance.CurrentPlayer.GetComponent<PhysicsBasedCharacterController>().JumpInputAction(context);
        }

        public void AttackInputAction(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            PlayerManager.Instance.CurrentPlayer.GetComponent<Players.Player>().Attack();
            UIManager.Instance.RefreshAmmunition();
        }

        public void SwitchActionMap(string newActionMap)
        {
            _previousActionMap = _playerInput.currentActionMap;
            _playerInput.SwitchCurrentActionMap(newActionMap);
        }

        public void RevertActionMap()
        {
            _playerInput.SwitchCurrentActionMap(_previousActionMap.name);
            _previousActionMap = _playerInput.currentActionMap;
        }
    }
}
