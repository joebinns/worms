using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Cameras;
using Items;
using Items.Hats;
using Items.Weapons;
using Players.Physics_Based_Character_Controller;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Players
{
    public class Player : MonoBehaviour
    {
        #region Player Settings
        [Header("Player Settings")]
        [SerializeField] private PlayerSettings _playerSettings;
        public int id => _playerSettings.ID;
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
        #endregion

        #region Renderers
        [Header("Renderers")]
        private List<Transform> _allRenderers = new List<Transform>();
        [SerializeField] private Transform _renderers;
        [SerializeField] private GameObject _hat;
        public GameObject Hat => _hat;
        [SerializeField] private Transform _hatSlot;
        [SerializeField] private ItemRack _weaponRack;
        public ItemRack WeaponRack => _weaponRack;
        [SerializeField] private Material _ditherMaterial;
        [SerializeField] private Material _flashMaterial;
        private const float FLASH_DURATION = 0.25f;
        #endregion
        
        #region Particle Systems
        [Header("Particle Systems")]
        [SerializeField] private ParticleSystem _dustParticleSystem;
        #endregion
        
        #region UI
        [Header("UI")]
        [SerializeField] private Sprite _portrait;
        public Sprite Portrait => _portrait;
        [SerializeField] private Sprite _deadPortrait;
        public Sprite DeadPortrait => _deadPortrait;
        
        [SerializeField] private Nameplate _nameplate;
        
        [SerializeField] private Transform _follower;
        #endregion

        #region Controller
        [Header("Controller")]
        [SerializeField] private PhysicsBasedCharacterController _physicsBasedCharacterController;
        #endregion

        private void Awake()
        {
            if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndices.PLAYER_SELECT)
            {
                _playerSettings.RestoreDefaults();
            }
        
            _physicsBasedCharacterController = GetComponent<PhysicsBasedCharacterController>();
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
            _weaponRack.CurrentItem.GetComponent<Weapon>().Attack();
        } 

        public void ChangeName(string newName)
        {
            name = newName;

            // Update nameplate
            if (_nameplate != null)
            {
                _nameplate.ChangeName(name);
            }
        }

        public void ChangeHat(GameObject newHat)
        {
            if (_hat != null)
            {
                Destroy(_hat);
            }
            _hat = Instantiate(newHat, _hatSlot);

            // Change hat to have the player's layer (due to dither shader)
            UpdateAllRenderers();
            SetRenderersLayerMask(LayerMask.LayerToName(this.gameObject.layer));
        }

        public void SetLookDirectionOption(PhysicsBasedCharacterController.LookDirectionOptions option)
        {
            _physicsBasedCharacterController._characterLookDirection = option;
        }

        public void KnockbackEffects(int _)
        {
            AudioManager.Instance.Play("Punch");
            StartCoroutine(FlashMaterial(_flashMaterial));
            StartCoroutine(FlashRendererSize(1.2f));
            CinemachineShake.Instance.ShakeCamera(20f, FLASH_DURATION);
        }

        private IEnumerator FlashRendererSize(float size)
        {
            _renderers.transform.localScale = Vector3.one * size;

            yield return new WaitForSeconds(FLASH_DURATION);

            _renderers.transform.localScale = Vector3.one;
        }

        private IEnumerator FlashMaterial(Material material)
        {
            ChangeMaterials(material);

            yield return new WaitForSeconds(FLASH_DURATION);

            RestoreDefaultMaterials();
        }

        public void EnableParticleSystem()
        {
            ParticleSystem.EmissionModule emission;
            emission = _dustParticleSystem.emission; // Stores the module in a local variable
            emission.enabled = true; // Applies the new value directly to the Particle System
        }

        public void DisableParticleSystem()
        {
            ParticleSystem.EmissionModule emission;
            emission = _dustParticleSystem.emission; // Stores the module in a local variable
            emission.enabled = false; // Applies the new value directly to the Particle System
        }

        public void AdjustRideHeight(float rideHeight)
        {
            _physicsBasedCharacterController._rideHeight = rideHeight;
        }

        private void UpdateAllRenderers()
        {
            _allRenderers.Clear();
            _allRenderers.Add(_renderers);
            int layerMask =~ LayerMask.GetMask("Projectile");
            UnityUtils.GetAllChildren(_renderers.transform, ref _allRenderers, layerMask);
        }

        public void EnableDitherMode()
        {
            ChangeMaterials(_ditherMaterial);
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

                transform.GetComponent<Renderer>().material = materialStorage.DefaultMaterial;
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
            _playerSettings.hat = _hat.GetComponent<Hat>().HatSettings;
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
}