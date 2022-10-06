using System.Collections;
using System.Collections.Generic;
using Player;
using Player.Physics_Based_Character_Controller;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aiming : MonoBehaviour
{
    private Vector2 _cursorDelta;
    private Vector2 _cursorPosition = Vector2.zero;

    private PhysicsBasedCharacterController _characterController;

    private Vector2 _sensitivity = Vector2.one * 0.03f;

    private void Start()
    {
        _characterController = PlayerManager.Instance.currentPlayer.GetComponent<PhysicsBasedCharacterController>();
    }

    private void OnEnable()
    {
        if (PlayerManager.Instance.currentPlayer == null)
        {
            return;
        }

        _characterController = PlayerManager.Instance.currentPlayer.GetComponent<PhysicsBasedCharacterController>();

        _cursorPosition = Vector2.zero;

        _characterController._characterLookDirection = PhysicsBasedCharacterController.LookDirectionOptions.Aiming;

    }

    private void OnDisable()
    {
        aimingInputVec3.y = 0;
        _characterController._aimingInput = aimingInputVec3;

        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }
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

    private Vector3 aimingInputVec3;

    private void Update()
    {
        _cursorPosition += _cursorDelta; // Don't multiply this by Time.deltaTime, as sensitivity should NOT be framerate dependent.
        _cursorPosition.y = Mathf.Clamp(_cursorPosition.y, -100f/_sensitivity.y, 100f/_sensitivity.y);

        var aimingInput = MathsUtils.MultiplyRows(_cursorPosition * Mathf.Deg2Rad, _sensitivity);
        aimingInputVec3 = new Vector3(aimingInput.x, aimingInput.y, aimingInput.x);

        _characterController._aimingInput = aimingInputVec3;

    }
}
