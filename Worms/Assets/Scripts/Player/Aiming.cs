using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aiming : MonoBehaviour
{
    private Vector2 _cursorDelta;
    private Vector2 _cursorPosition = Vector2.zero;

    private PhysicsBasedCharacterController _characterController;

    private Vector2 _sensitivity = Vector2.one * 20f;

    private void Start()
    {
        _characterController = PlayerManager.currentPlayer.GetComponent<PhysicsBasedCharacterController>();
    }

    private void OnEnable()
    {
        if (PlayerManager.currentPlayer == null)
        {
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;

        _characterController = PlayerManager.currentPlayer.GetComponent<PhysicsBasedCharacterController>();

        _cursorPosition = Vector2.zero;

        _characterController._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.aiming;

    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;

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

        _characterController._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.velocity;
    }

    public void CursorDelta(InputAction.CallbackContext context)
    {
        _cursorDelta = context.ReadValue<Vector2>();
    }

    private Vector3 aimingInputVec3;

    private void Update()
    {
        _cursorPosition += _cursorDelta * Time.deltaTime;
        _cursorPosition.y = Mathf.Clamp(_cursorPosition.y, -100f/_sensitivity.y, 100f/_sensitivity.y);

        var aimingInput = MathsUtils.MultiplyRows(_cursorPosition * Mathf.Deg2Rad, _sensitivity);
        aimingInputVec3 = new Vector3(aimingInput.x, aimingInput.y, aimingInput.x);

        _characterController._aimingInput = aimingInputVec3;

    }
}
