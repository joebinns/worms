using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static PlayerInput _playerInput;

    private static InputActionMap _previousActionMap;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _previousActionMap = _playerInput.currentActionMap;
    }
    
    public void PrimaryHotbarSlotInputAction(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        
        // Change weapon rack item, change hotbar icon
        PlayerManager.currentPlayer.GetComponent<Player>().weaponRack.ChangeItem(1);
        UIManager.SwitchActiveHotbar(1);
    }
    
    public void SecondaryHotbarSlotInputAction(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        // Change weapon rack item, change hotbar icon
        PlayerManager.currentPlayer.GetComponent<Player>().weaponRack.ChangeItem(2);
        UIManager.SwitchActiveHotbar(2);
    }

    public void EmptyHotbarSlotInputAction(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        // Change weapon rack item, change hotbar icon
        PlayerManager.currentPlayer.GetComponent<Player>().weaponRack.ChangeItem(0);
        UIManager.SwitchActiveHotbar(0);
    }

    public void MoveInputAction(InputAction.CallbackContext context)
    {
        PlayerManager.currentPlayer.GetComponent<PhysicsBasedCharacterController>().MoveInputAction(context);
    }

    public void JumpInputAction(InputAction.CallbackContext context)
    {
        PlayerManager.currentPlayer.GetComponent<PhysicsBasedCharacterController>().JumpInputAction(context);
    }

    public void AttackInputAction(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        PlayerManager.currentPlayer.GetComponent<Player>().Attack();
    }

    public static void SwitchActionMap(string newActionMap)
    {
        _previousActionMap = _playerInput.currentActionMap;
        _playerInput.SwitchCurrentActionMap(newActionMap);
    }

    public static void RevertActionMap()
    {
        _playerInput.SwitchCurrentActionMap(_previousActionMap.name);
        _previousActionMap = _playerInput.currentActionMap;
    }
}
