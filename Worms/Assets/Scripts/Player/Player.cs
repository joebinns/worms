using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;

    public string playerName;

    public HatScriptableObject hat;

    public PlayerState state;
    
    public static event Action<PlayerState> OnPlayerStateChanged;

    public Sprite portrait;

    public Material ditherMaterial;

    public Material jumpsuitMaterial;
    public Material visorMaterial;

    public GameObject jumpsuit;
    public GameObject visor;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
