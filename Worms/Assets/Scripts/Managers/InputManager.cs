using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static PlayerInput _playerInput; 
    
    private static PlayerControls _playerControls;

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
        }
        
        _playerControls.Enable();
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
    
    public static void UnsubscribeFromMovement(Player player)
    {
        _playerControls.Moving.Move.started -= context => physicsBasedCharacterController.MoveInputAction(context);
        _playerControls.Moving.Move.performed -= context => physicsBasedCharacterController.MoveInputAction(context);
        _playerControls.Moving.Move.canceled -= context => physicsBasedCharacterController.MoveInputAction(context);
        
        _playerControls.Moving.Jump.started -= context => physicsBasedCharacterController.JumpInputAction(context);
        _playerControls.Moving.Jump.performed -= context => physicsBasedCharacterController.JumpInputAction(context);
        _playerControls.Moving.Jump.canceled -= context => physicsBasedCharacterController.JumpInputAction(context);
    }
    
    private static PhysicsBasedCharacterController physicsBasedCharacterController;

    public static void SubscribeToMovement(Player player)
    {
        physicsBasedCharacterController = player.GetComponent<PhysicsBasedCharacterController>();
        
        _playerControls.Moving.Move.started += context => physicsBasedCharacterController.MoveInputAction(context);
        _playerControls.Moving.Move.performed += context => physicsBasedCharacterController.MoveInputAction(context);
        _playerControls.Moving.Move.canceled += context => physicsBasedCharacterController.MoveInputAction(context);
        
        _playerControls.Moving.Jump.started += context => physicsBasedCharacterController.JumpInputAction(context);
        _playerControls.Moving.Jump.performed += context => physicsBasedCharacterController.JumpInputAction(context);
        _playerControls.Moving.Jump.canceled += context => physicsBasedCharacterController.JumpInputAction(context);
    }

    public static void EnableAimingActionMap()
    {
        _playerInput.actions.FindActionMap("Aiming").Enable();
    }
    
    public static void DisableAimingActionMap()
    {
        _playerInput.actions.FindActionMap("Aiming").Disable();
    }

    public static void EnableMovingActionMap()
    {
        _playerInput.actions.FindActionMap("Moving").Enable();
        InputManager.SubscribeToMovement(PlayerManager.currentPlayer);
    }
    
    public static void DisableMovingActionMap()
    {
        _playerInput.actions.FindActionMap("Moving").Disable();
        InputManager.UnsubscribeFromMovement(PlayerManager.currentPlayer);
    }
    
    
}
