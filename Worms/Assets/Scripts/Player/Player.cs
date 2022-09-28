using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Other")]
    public int id;
    public string playerName = "Player Name Undefined";
    public PlayerSettings playerSettings;
    public Transform follower;
    public Weapon currentWeapon;

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
    public TextMeshProUGUI namePlateText;

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
        UnpackPlayerSettings();
    }

    private void Start()
    {
        //UnpackPlayerSettings();
    }

/*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            currentWeapon.Attack();
        }
    }
    */

    public void ChangeHat(GameObject newHat)
    {
        if (hat != null)
        {
            Destroy(hat);
        }
        hat = Instantiate(newHat, hatSlot);
        
        // Change hat to have the player's layer (due to dither shader)
        hat.layer = renderers.gameObject.layer;
        foreach (Transform child in hat.transform)
        {
            child.gameObject.layer = renderers.gameObject.layer;
        }
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

    public void EditPlayerSettings(string name, GameObject hat)
    {
        if (name == "")
        {
            name = playerSettings.suggestedName;
        }
        playerName = name;
        ChangeHat(hat);

        PackPlayerSettings();
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
        //Debug.LogError(playerName);
        ChangeHat(playerSettings.hat.prefab);
    }

    private void OnDestroy()
    {
        if (follower != null)
        {
            Destroy(follower.gameObject);
        }
    }
}

public enum PlayerState
{
    Moving,
    Aiming,
    Dead
}
