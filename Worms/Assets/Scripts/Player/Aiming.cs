using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aiming : MonoBehaviour
{
    private Vector2 _cursorDelta;
    //private Vector3 _cursorOffset = Vector3.zero;
    private Vector2 _cursorPosition = Vector2.zero;

    private PhysicsBasedCharacterController _characterController;

    private Vector2 _sensitivity = Vector2.one * 25f;

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
        //_cursorOffset = velocity;

        //Debug.Log(_cursorOffset);

        _characterController._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.aiming;

        

        // Make the _characterController look spring much faster and less dampened... hmmm... bit janky
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;

        _characterController._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.velocity;

    }

    public void CursorDelta(InputAction.CallbackContext context)
    {
        _cursorDelta = context.ReadValue<Vector2>(); // Actually, this is DELTA now
    }

    private void Update()
    {
        _cursorPosition += _cursorDelta * Time.deltaTime;
        _cursorPosition.y = Mathf.Clamp(_cursorPosition.y, -2.5f, 2.5f);

        var aimingInput = MathsUtils.MultiplyRows(_cursorPosition * Mathf.Deg2Rad, _sensitivity);
        var aimingInputVec3 = new Vector3(aimingInput.x, aimingInput.y, aimingInput.x);
        //var aimingInput = new Vector3(Mathf.Sin(adjustedCursor.x), adjustedCursor.y, Mathf.Cos(adjustedCursor.x));

        _characterController._aimingInput = aimingInputVec3; // I want to somehow bypass the character controller's spring system...

    }

    //lookDirec
}
