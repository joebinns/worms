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

    [Header("Renderers")]
    public Transform renderers;
    public GameObject hat;
    public Transform hatSlot;
    //public List<GameObject> weapons;
    //public int currentWeaponIndex;
    public ItemRack weaponRack;
    //public GameObject weapon;
    public Transform weaponSlot;
    public Material ditherMaterial;
    public Sprite portrait;

    //public TextMeshProUGUI namePlateText;
    public Nameplate nameplate;

    [Header("Particle Systems")]
    public ParticleSystem dustParticleSystem;

    // Events
    public static event Action<PlayerState> OnPlayerStateChanged;

    // Accessors
    private PhysicsBasedCharacterController physicsBasedCharacterController;

    // Variables
    private PlayerState state;
    private List<Transform> _allRenderers = new List<Transform>();

    private void Awake()
    {
        physicsBasedCharacterController = GetComponent<PhysicsBasedCharacterController>();
        UnpackPlayerSettings();
        UpdateAllRenderers();
    }

    public void Attack()
    {
        //currentWeapon.Equip(this);
        //weapon.GetComponent<Weapon>().weaponSettings.Attack();
        weaponRack.currentItem.GetComponent<Weapon>().weaponSettings.Attack();
    } 

    public void PickUp()
    {

    }

    public void ChangeName(string newName)
    {
        playerName = newName;

        // Update nameplate
        if (nameplate != null)
        {
            nameplate.ChangeName(playerName);
        }

    }

    /*
    public void ChangeWeapon(GameObject newWeapon)
    {
        // Have all equipped weapons in a list, cycle between activating in the list (like hatrack)
        weapons[currentWeaponIndex].SetActive(false);
        
        
        
        
        if (weapon != null)
        {
            Destroy(weapon);
        }
        weapon = Instantiate(newWeapon, weaponSlot);

        UpdateAllRenderers();
        SetRenderersLayerMask(LayerMask.LayerToName(this.gameObject.layer));
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
        UpdateAllRenderers();
        SetRenderersLayerMask(LayerMask.LayerToName(this.gameObject.layer));
    }

    /*
    public void EquipWeapon(GameObject newWeapon) // Is it better to have this here or in the weapon SO? 
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject); // How can I rework this to work with the scriptable object...
        }
        weapon = Instantiate(newWeapon, weaponSlot);

        // Change hat to have the player's layer (due to dither shader)
        UpdateAllRenderers();
        SetRenderersLayerMask(LayerMask.LayerToName(this.gameObject.layer));
    }
    */

    public void SetLookDirectionOption(PhysicsBasedCharacterController.lookDirectionOptions option)
    {
        physicsBasedCharacterController._characterLookDirection = option;
    }

    public void EnableParticleSystem()
    {
        ParticleSystem.EmissionModule emission;
        emission = dustParticleSystem.emission; // Stores the module in a local variable
        emission.enabled = true; // Applies the new value directly to the Particle System

        //dustParticleSystem.transform.parent.gameObject.SetActive(true);
    }

    public void DisableParticleSystem()
    {
        ParticleSystem.EmissionModule emission;
        emission = dustParticleSystem.emission; // Stores the module in a local variable
        emission.enabled = false; // Applies the new value directly to the Particle System

        //dustParticleSystem.transform.parent.gameObject.SetActive(false);
    }

    public void AdjustRideHeight(float rideHeight)
    {
        physicsBasedCharacterController._rideHeight = rideHeight;
    }

    private void UpdateAllRenderers()
    {
        _allRenderers.Clear();
        _allRenderers.Add(renderers);
        UnityUtils.GetAllChildren(renderers.transform, ref _allRenderers);
    }

    public void EnableDitherMode()
    {
        UpdateAllRenderers();

        foreach (Transform transform in _allRenderers)
        {
            var renderer = transform.GetComponent<Renderer>();
            if (renderer == null)
            {
                continue;
            }

            renderer.material = ditherMaterial;
        }
    }

    public void DisableDitherMode()
    {
        UpdateAllRenderers(); // Not sure why this is needed here, since it already gets called when new hats are instantiated.

        foreach (Transform transform in _allRenderers)
        {
            var materialStorage = transform.GetComponent<MaterialStorage>();
            if (materialStorage == null)
            {
                continue;
            }

            transform.GetComponent<Renderer>().material = materialStorage.defaultMaterial;
        }
    }

    private void SetRenderersLayerMask(string layerName)
    {
        var newLayer = LayerMask.NameToLayer(layerName);
        foreach (Transform transform in _allRenderers)
        {
            transform.gameObject.layer = newLayer;
        }
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
                physicsBasedCharacterController.enabled = false;
                
                EnableDitherMode();

                DisableParticleSystem();

                SetRenderersLayerMask("Default");

                StartCoroutine(nameplate.Hide(0.25f));

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
        ChangeName(playerSettings.name);
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
