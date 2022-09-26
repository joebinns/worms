using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Other")]
    public int id;
    public string playerName = "Player Name Undefined";
    public bool hasUserEdits = false;
    public PlayerSettings playerSettings;
    //public HatSettings hatSettings;

    [Header("Renderers")]
    public Transform renderers;
    public GameObject hat;
    public Transform hatSlot;
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
        physicsBasedCharacterController = GetComponent<PhysicsBasedCharacterController>();
    }

    private void Start()
    {
        UnpackPlayerSettings();
    }

    public void SetHat(GameObject newHat)
    {
        // Delete previous hat
        Destroy(hat);

        // Instantiate new hat
        hat = Instantiate(newHat, renderers);
        hat.transform.localPosition = Vector3.zero;

        hat.layer = renderers.gameObject.layer;
        foreach (Transform child in hat.transform)
        {
            child.gameObject.layer = renderers.gameObject.layer;
        }

        hat.transform.localPosition += Vector3.up;

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

    public void PackPlayerSettings()
    {
        playerSettings.id = id;
        playerSettings.name = playerName;
        playerSettings.hat = hat.GetComponent<Hat>().hatSettings;
    }

    public void UnpackPlayerSettings()
    {
        id = playerSettings.id;
        playerName = playerSettings.name;
        hat = Instantiate(playerSettings.hat.prefab, hatSlot);
    }

}

public enum PlayerState
{
    Moving,
    Aiming,
    Dead
}
