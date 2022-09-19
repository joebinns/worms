using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    private static int _turn = 0;
    
    public static void NextTurn(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        _turn++;
     
        PlayerManager.SetCurrentPlayer(_turn % PlayerManager.numPlayers);
        CameraManager.UpdateCameraState(CameraState.FollowCamera);

    }
    
}


