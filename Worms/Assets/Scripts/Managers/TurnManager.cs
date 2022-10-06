using System;
using System.Collections;
using System.Collections.Generic;
using Items.Weapons;
using Managers;
using Players;
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
        var newPlayer = PlayerManager.Instance.SetCurrentPlayer(playerIndex);
  
        // Reset new player's ammo
        foreach (GameObject item in newPlayer.WeaponRack.Items)
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
        var currentIndex = PlayerManager.Instance.IdToIndex(PlayerManager.Instance.CurrentPlayer.id);
        var nextIndex = (currentIndex + 1) % PlayerManager.Instance.NumPlayers;

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


