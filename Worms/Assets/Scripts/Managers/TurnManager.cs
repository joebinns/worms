using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    private static int _turn = 0;

    private void OnEnable()
    {
        CountdownTimer.OnCountedDown += NextTurn;
    }

    private void OnDisable()
    {
        CountdownTimer.OnCountedDown -= NextTurn;
    }

    public static void RefreshTurn()
    {
        CameraManager.UpdateCameraState(CameraState.FollowCamera);
        PlayerManager.SetCurrentPlayer(_turn % PlayerManager.numPlayers);
    }

    public static void NextTurn()
    {
        _turn++;

        RefreshTurn();
    }
    
    public static void NextTurnInputAction(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        NextTurn(); 

    }
    
}


