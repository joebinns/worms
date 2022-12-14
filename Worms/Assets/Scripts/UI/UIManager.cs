using Cameras;
using Players;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton
        public static UIManager Instance;
        #endregion
        
        private Reticle _reticle;
        private Portraits _portraits;
        private UIRack _hotbar;
        private CountdownTimer _turnTimer;
        private Ammunition _ammunition;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
            _reticle = FindObjectOfType<Reticle>();
            _portraits = FindObjectOfType<Portraits>();
            _hotbar = FindObjectOfType<UIRack>(); // May this also find Portraits accidentally, due to inheritance?
            _turnTimer = FindObjectOfType<CountdownTimer>();
            _ammunition = FindObjectOfType<Ammunition>();

            DisableReticle();
        }

        private void OnEnable()
        {
            PlayerManager.OnCurrentPlayerChanged += SwitchActivePortrait;
            PlayerManager.OnCurrentPlayerChanged += ResetTurnTimer;
            PlayerManager.OnPlayerRemoved += DisablePortrait;
            PlayerManager.OnLastPlayerStanding += NextScene;
            CameraManager.OnCameraStateChanged += ChangeHUDMode;
        }

            private void OnDisable()
        {
            PlayerManager.OnCurrentPlayerChanged -= SwitchActivePortrait;
            PlayerManager.OnCurrentPlayerChanged -= ResetTurnTimer;
            PlayerManager.OnPlayerRemoved -= DisablePortrait;
            PlayerManager.OnLastPlayerStanding -= NextScene;
            CameraManager.OnCameraStateChanged -= ChangeHUDMode;
        }

            private void Start()
        {
            HideHotbar();
        }
        
        private void ChangeHUDMode(CameraState state)
        {
            switch (state)
            {
                case CameraState.FollowCamera:
                    DisableReticle();
                    HideHotbar();
                    break;
                case CameraState.AimCamera:
                    EnableReticle();
                    ShowHotbar();
                    break;
            }
        }

        public void EnableReticle()
        {
            _reticle.gameObject.SetActive(true);
        }
    
        public void DisableReticle()
        {
            _reticle.gameObject.SetActive(false);
        }

        public void ToggleReticleZoom(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                return;
            }

            _reticle.ToggleZoom(); // started or canceled
        }

        public void SwitchActiveHotbar(int id)
        {
            _hotbar.SwitchActive(id);
            // Update ammo
        }

        public void SwitchActivePortrait(Players.Player player)
        {
            _portraits.SwitchActive(player.id);
        }

        public void DisablePortrait(Players.Player player)
        {
            _portraits.DisablePortrait(player);
        }

        public void RefreshAmmunition() // Called on weapon switch and on attack
        {
            _ammunition.RefreshDisplay();
        }
        
        public void ResetTurnTimer(Player player)
        {
            _turnTimer.ResetTimer();
        }

        public void HideHotbar()
        {
            // NOTE: disabling the hotbar game object can cause problems due to the order of execution of HideHotbar()
            // being inconsistent
            //_hotbar.gameObject.SetActive(false);
            _hotbar.GetComponent<CanvasGroup>().alpha = 0;
            HideAmmunition();
        }

        public void ShowHotbar()
        {
            //_hotbar.gameObject.SetActive(true);
            _hotbar.GetComponent<CanvasGroup>().alpha = 1;
        }

        public void HideAmmunition()
        {
            _ammunition.gameObject.SetActive(false);
        }
    
        public void ShowAmmunition()
        {
            _ammunition.gameObject.SetActive(true);
        }

        public void NextScene()
        {
            LoadingScreen.Instance.ChangeSceneImpatient(SceneIndices.VICTORY);
        }

    }
}
