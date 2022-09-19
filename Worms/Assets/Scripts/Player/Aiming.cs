using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aiming : MonoBehaviour
{
    // Whilst aiming:
    // 1.   Change PhysicsBasedCharacterController _characterLookDirection (option).
    // 2.   Create a new lookDirectionOptions option for looking in mouse.

    // Zoom:
    // 1.   Press shift
    // 2.   Move camera forward? (or reduce fov?)
    // 3.   Shrink reticle

    private Vector2 _lookPosition;
    private Vector2 _mouseOffset = Vector2.zero;
    private Vector2 _mousePosition = Vector2.zero;

    private PhysicsBasedCharacterController _characterController;

    private void Start()
    {
        _characterController = PlayerManager.currentPlayer.GetComponent<PhysicsBasedCharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        if (PlayerManager.currentPlayer == null)
        {
            return;
        }

        _characterController = PlayerManager.currentPlayer.GetComponent<PhysicsBasedCharacterController>();
        // Set _characterLookDirection to aiming
        _characterController._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.aiming;
        // Reset mouse position to centre ?
        //_mouseOffset = _lookPosition; // Only use mouseOffset if Keyboard and Mouse is being used
        _mousePosition = Vector2.zero;

        // Make the _characterController look spring much faster and less dampened... hmmm... bit janky
    }

    private void OnDisable()
    {
        _characterController._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.velocity;

    }

    public void LookPosition(InputAction.CallbackContext context)
    {
        _lookPosition = context.ReadValue<Vector2>(); // Actually, this is DELTA now
    }

    private void Update()
    {
        //Debug.Log(_mousePosition);

        _mousePosition += _lookPosition * Time.deltaTime;
        // HMMM... using mouse delta works well. BUT, doesn't work when the mouse ISN'T moving...

        // Somehow now need to allow aimingInput to loop... get _lookPosition in a different way?
        var xSensitivity = 0.01f * 1000f;
        var ySensitivity = 0.1f * 1000f;
        // _mouseOffset.y doesn't seem to be working for some reason... (?)
        var aimingInput = new Vector3(Mathf.Sin((_mousePosition.x) / 360f * 2 * Mathf.PI * xSensitivity), (_mousePosition.y) / 360f * ySensitivity, Mathf.Cos((_mousePosition.x)  / 360f * 2 * Mathf.PI  * xSensitivity));
        //Debug.Log(aimingInput);
        _characterController._aimingInput = aimingInput;

    }

    //lookDirec
}
