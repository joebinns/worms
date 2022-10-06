using System.Collections;
using System.Collections.Generic;
using Players;
using Players.Physics_Based_Character_Controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    public class Aiming : MonoBehaviour
    {
        private Vector3 _aimingInputVec3;
        private Vector2 _cursorDelta;
        private Vector2 _cursorPosition;

        private PhysicsBasedCharacterController _characterController;

        [SerializeField] private Vector2 _sensitivity = Vector2.one * 0.03f;

        private void OnEnable()
        {
            if (PlayerManager.Instance.CurrentPlayer == null) { return; }
            
            StartAiming();
        }

        private void OnDisable()
        {
            StopAiming();
        }
        
        private void StartAiming()
        {
            _characterController = PlayerManager.Instance.CurrentPlayer.GetComponent<PhysicsBasedCharacterController>();
            _cursorPosition = Vector2.zero;
            _characterController._characterLookDirection = PhysicsBasedCharacterController.LookDirectionOptions.Aiming;
        }
        
        private void StopAiming()
        {
            _aimingInputVec3.y = 0;
            _characterController._aimingInput = _aimingInputVec3;

            if (!this.gameObject.activeInHierarchy) { return; }
            StartCoroutine(RevertCharacterLookDirectionAfterFixedUpdate());
        }

        private IEnumerator RevertCharacterLookDirectionAfterFixedUpdate()
        {
            yield return new WaitForFixedUpdate();
            _characterController._characterLookDirection = PhysicsBasedCharacterController.LookDirectionOptions.Velocity;
        }

        public void CursorDelta(InputAction.CallbackContext context)
        {
            _cursorDelta = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            _cursorPosition += _cursorDelta; // Don't multiply this by Time.deltaTime, as sensitivity should NOT be framerate dependent.
            _cursorPosition.y = Mathf.Clamp(_cursorPosition.y, -100f/_sensitivity.y, 100f/_sensitivity.y);

            var aimingInput = MathsUtils.MultiplyRows(_cursorPosition * Mathf.Deg2Rad, _sensitivity);
            _aimingInputVec3 = new Vector3(aimingInput.x, aimingInput.y, aimingInput.x);

            _characterController._aimingInput = _aimingInputVec3;
        }
    }
}