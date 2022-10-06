using System;
using Camera;
using Players;
using UnityEngine;
using UnityEngine.InputSystem;
using CursorMode = Camera.CursorMode;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;
    
        private static Animator animator;

        private static Aiming aiming;

        public static CameraState state;

        public static CameraZoom followCameraZoom;
    
        public static event Action<CameraState> OnCameraStateChanged;

        private void Awake()
        {
            Instance = this;
            animator = GetComponent<Animator>();
            aiming = FindObjectOfType<Aiming>();
            followCameraZoom = GameObject.Find("Follow Camera").GetComponent<CameraZoom>();
        }
    
        void Start()
        {
            CursorMode.DisableCursor();
            UpdateCameraState(CameraState.FollowCamera);
        }

        public static void UpdateCameraState(CameraState cameraState)
        {
            if (state == cameraState)
            {
                return;
            }

            state = cameraState;
        
            switch (state)
            {
                case CameraState.FollowCamera:
                    animator.Play("Follow Camera"); // Switch state driven camera to use Follow Camera
                    InputManager.SwitchActionMap("Moving");
                    followCameraZoom.ResetZoom();
                    aiming.enabled = false;
                    UIManager.DisableReticle();
                    InputManager.SwapWeapon(0);
                    UIManager.HideHotbar();
                    break;
                case CameraState.AimCamera:
                    animator.Play("Aim Camera"); // Switch state driven camera to use Aim Camera
                    InputManager.SwitchActionMap("Aiming");
                    aiming.enabled = true;
                    UIManager.EnableReticle();
                    UIManager.ShowHotbar();
                    break;
            }

            OnCameraStateChanged?.Invoke(state);
        }
    
        public static void ToggleAimAction(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            if (state == CameraState.FollowCamera)
            {
                UpdateCameraState(CameraState.AimCamera);
            }
            else
            {
                UpdateCameraState(CameraState.FollowCamera);
            }
        
        }
    }

    public enum CameraState
    {
        FollowCamera,
        AimCamera
    };
}