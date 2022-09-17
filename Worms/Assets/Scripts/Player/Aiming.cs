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
    private Vector2 _mouseOffset;

    private PhysicsBasedCharacterController _characterController;

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

        _characterController = PlayerManager.currentPlayer.GetComponent<PhysicsBasedCharacterController>();
        // Set _characterLookDirection to aiming
        _characterController._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.aiming;
        // Reset mouse position to centre ?
        _mouseOffset = _lookPosition; // Only use mouseOffset if Keyboard and Mouse is being used
    }

    private void OnDisable()
    {
        _characterController._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.velocity;

    }

    public void LookPosition(InputAction.CallbackContext context)
    {
        _lookPosition = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // Somehow now need to allow aimingInput to loop... get _lookPosition in a different way?
        var xSensitivity = 0.01f;
        var ySensitivity = 0.1f;
        Debug.Log(_mouseOffset);
        // _mouseOffset.y doesn't seem to be working for some reason... (?)
        var aimingInput = new Vector3(Mathf.Sin((_lookPosition.x - _mouseOffset.x) / 360f * 2 * Mathf.PI * xSensitivity), (_lookPosition.y -_mouseOffset.y) / 360f * ySensitivity, Mathf.Cos((_lookPosition.x - _mouseOffset.x)  / 360f * 2 * Mathf.PI  * xSensitivity));
        //Debug.Log(aimingInput);
        _characterController._aimingInput = aimingInput;

    }

    //lookDirec
}
