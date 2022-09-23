using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Other")]
    public int id;
    public string playerName = "Player Name Undefined";

    [Header("Renderers")]
    public GameObject jumpsuit; // these vars should be made serialized private...
    public Material jumpsuitMaterial;
    public GameObject visor;
    public Material visorMaterial;
    public Material ditherMaterial;
    public Sprite portrait;

    [Header("Particle Systems")]
    public ParticleSystem dustParticleSystem;

    // Events
    public static event Action<PlayerState> OnPlayerStateChanged;

    // Accessors
    private PhysicsBasedCharacterController physicsBasedCharacterController;

    // Variables
    private PlayerState state;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        physicsBasedCharacterController = GetComponent<PhysicsBasedCharacterController>();
    }

    public void SetLookDirectionOption(PhysicsBasedCharacterController.lookDirectionOptions option)
    {
        physicsBasedCharacterController._characterLookDirection = option;
    }

    public void EnableParticleSystem()
    {
        dustParticleSystem.transform.parent.gameObject.SetActive(true);
    }

    public void DisableParticleSystem()
    {
        dustParticleSystem.transform.parent.gameObject.SetActive(false);
    }

    public void AdjustRideHeight(float rideHeight)
    {
        physicsBasedCharacterController._rideHeight = rideHeight;
    }

    public void EnableDitherMode()
    {
        jumpsuit.GetComponent<Renderer>().material = ditherMaterial;
        visor.GetComponent<Renderer>().material = ditherMaterial;
    }

    public void DisableDitherMode()
    {
        jumpsuit.GetComponent<Renderer>().material = jumpsuitMaterial;
        visor.GetComponent<Renderer>().material = visorMaterial;
    }

    public void UpdatePlayerState(PlayerState playerState)
    {
        state = playerState;
        switch (playerState)
        {
            case PlayerState.Moving:
                physicsBasedCharacterController._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.velocity;
                break;
            case PlayerState.Aiming:
                break;
            case PlayerState.Dead:
                break;
        }
        OnPlayerStateChanged?.Invoke(state);
    }
}

public enum PlayerState
{
    Moving,
    Aiming,
    Dead
}
