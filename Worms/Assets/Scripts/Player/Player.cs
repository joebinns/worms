using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Camera;
using Items;
using Items.Hats;
using Items.Weapons;
using Player.Physics_Based_Character_Controller;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Player Settings")]
        private PlayerSettings _playerSettings;
        public int id {
            get => _playerSettings.ID;
        }
        public bool shouldSpawn
        {
            get => _playerSettings.shouldSpawn;
            set => _playerSettings.shouldSpawn = value;
        }
        public string suggestedName => _playerSettings.SuggestedName;
        public new string name
        {
            get => _playerSettings.name;
            private set => _playerSettings.name = value;
        }

        [Header("Renderers")]
        public Transform renderers;
        public GameObject hat;
        public Transform hatSlot;
        public ItemRack weaponRack;
        public Transform weaponSlot;
        public Material ditherMaterial;
        public Material flashMaterial;
        public float flashDuration = 0.15f;
        public Sprite portrait;
        public Sprite deadPortrait;

        //public TextMeshProUGUI namePlateText;
        public Nameplate nameplate;
        
        [SerializeField] private Transform _follower;

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
            if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndices.PLAYER_SELECT)
            {
                _playerSettings.RestoreDefaults();
            }
        
            physicsBasedCharacterController = GetComponent<PhysicsBasedCharacterController>();
            UnpackPlayerSettings();
            UpdateAllRenderers();
        }

        private void OnEnable()
        {
            GetComponent<Knockback>().OnKnockbackChanged += KnockbackEffects;
        }

        private void OnDisable()
        {
            GetComponent<Knockback>().OnKnockbackChanged -= KnockbackEffects;
        }

        public void Attack()
        {
            weaponRack.CurrentItem.GetComponent<Weapon>().Attack();

        } 

        public void ChangeName(string newName)
        {
            name = newName;

            // Update nameplate
            if (nameplate != null)
            {
                nameplate.ChangeName(name);
            }

        }

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

        public void SetLookDirectionOption(PhysicsBasedCharacterController.LookDirectionOptions option)
        {
            physicsBasedCharacterController._characterLookDirection = option;
        }

        public void KnockbackEffects(int _)
        {
            AudioManager.Instance.Play("Punch");
            StartCoroutine(FlashMaterial(flashMaterial));
            StartCoroutine(FlashRendererSize(1.2f));
            CinemachineShake.Instance.ShakeCamera(20f, flashDuration);
        }

        private IEnumerator FlashRendererSize(float size)
        {
            renderers.transform.localScale = Vector3.one * size;

            yield return new WaitForSeconds(flashDuration);

            renderers.transform.localScale = Vector3.one;
        }

        private IEnumerator FlashMaterial(Material material)
        {
            ChangeMaterials(material);

            yield return new WaitForSeconds(flashDuration);

            RestoreDefaultMaterials();
        }

        public void EnableParticleSystem()
        {
            ParticleSystem.EmissionModule emission;
            emission = dustParticleSystem.emission; // Stores the module in a local variable
            emission.enabled = true; // Applies the new value directly to the Particle System
        }

        public void DisableParticleSystem()
        {
            ParticleSystem.EmissionModule emission;
            emission = dustParticleSystem.emission; // Stores the module in a local variable
            emission.enabled = false; // Applies the new value directly to the Particle System
        }

        public void AdjustRideHeight(float rideHeight)
        {
            physicsBasedCharacterController._rideHeight = rideHeight;
        }

        private void UpdateAllRenderers()
        {
            _allRenderers.Clear();
            _allRenderers.Add(renderers);
            int layerMask =~ LayerMask.GetMask("Projectile");
            UnityUtils.GetAllChildren(renderers.transform, ref _allRenderers, layerMask);
        }

        public void EnableDitherMode()
        {
            ChangeMaterials(ditherMaterial);
        }

        public void RestoreDefaultMaterials()
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

        private void ChangeMaterials(Material material)
        {
            UpdateAllRenderers();

            foreach (Transform transform in _allRenderers)
            {
                var renderer = transform.GetComponent<Renderer>();
                if (renderer == null)
                {
                    continue;
                }

                renderer.material = material;
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
                    physicsBasedCharacterController._characterLookDirection = PhysicsBasedCharacterController.LookDirectionOptions.Velocity;
                    break;
                case PlayerState.Aiming:
                    break;
            }
            OnPlayerStateChanged?.Invoke(state);
        }

        public void EditPlayerSettings(string name, GameObject hat)
        {
            if (name == "")
            {
                name = _playerSettings.SuggestedName;
            }
            this.name = name;
            ChangeHat(hat);

            PackPlayerSettings();
        }

        public void PackPlayerSettings()
        {
            _playerSettings.name = name;
            _playerSettings.hat = hat.GetComponent<Hat>().HatSettings;
        }

        private void UnpackPlayerSettings()
        {
            ChangeName(_playerSettings.name);
            if (_playerSettings.hat != null)
            {
                ChangeHat(_playerSettings.hat.Prefab);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Death Trigger"))
            {
                PlayerManager.Instance.DeletePlayer(this);
            }
        }

        private void OnDestroy()
        {
            if (_follower != null)
            {
                Destroy(_follower.gameObject);
            }
        }
    }

    public enum PlayerState
    {
        Moving,
        Aiming
    }
}