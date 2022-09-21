using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static PlayerInput _playerInput;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        _playerInput = GetComponent<PlayerInput>();
    }

    public void MoveInputAction(InputAction.CallbackContext context)
    {
        PlayerManager.currentPlayer.GetComponent<PhysicsBasedCharacterController>().MoveInputAction(context);
    }

    public void JumpInputAction(InputAction.CallbackContext context)
    {
        PlayerManager.currentPlayer.GetComponent<PhysicsBasedCharacterController>().JumpInputAction(context);
    }

    public static void SwitchActionMap(string newActionMap)
    {
        _playerInput.SwitchCurrentActionMap(newActionMap);
    }
}
