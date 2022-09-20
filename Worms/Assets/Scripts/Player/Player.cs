using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int _id { get; private set; }
    
    public string name { get; set; }

    public HatScriptableObject hat;

    public PlayerState state;
    
    public static event Action<PlayerState> OnPlayerStateChanged;

    public Image portrait;


    public void UpdatePlayerState(PlayerState playerState)
    {
        state = playerState;

        switch (playerState)
        {
            case PlayerState.Moving:
                break;
            case PlayerState.Aiming:
                break;
            case PlayerState.Dead:
                break;
        }

        OnPlayerStateChanged?.Invoke(state); // '?.Invoke' prevent a Null error exception, in the case that there are no subscribers 
    }
}

public enum PlayerState
{
    Moving,
    Aiming,
    Dead
}
