using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    //private static int _turn = 0;

    private void OnEnable()
    {
        CountdownTimer.OnCountedDown += NextTurn;
    }

    private void OnDisable()
    {
        CountdownTimer.OnCountedDown -= NextTurn;
    }

    public static void ChangeTurn(int playerIndex)
    {
        CameraManager.UpdateCameraState(CameraState.FollowCamera);
        var newPlayer = PlayerManager.SetCurrentPlayer(playerIndex);
  
        // Reset new player's ammo
        foreach (GameObject item in newPlayer.weaponRack.items)
        {
            var weapon = item.GetComponent<Weapon>();

            if (weapon != null)
            {
                weapon.ResetAmmunition();
            }

        }  
    
    }

    public static void NextTurn()
    {
        var currentIndex = PlayerManager.IdToIndex(PlayerManager.currentPlayer.id);
        var nextIndex = (currentIndex + 1) % PlayerManager.numPlayers;

        // I think this works...
        ChangeTurn(nextIndex);
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


