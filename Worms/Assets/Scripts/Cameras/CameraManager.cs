using System;
using Cameras;
using Players;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using CursorMode = Cameras.CursorMode;

namespace Cameras
{
    public class CameraManager : MonoBehaviour
    {
        #region Singleton
        public static CameraManager Instance;
        #endregion
        
        [SerializeField] private CameraZoom _followCameraZoom;
        private Animator _animator;
        private Aiming _aiming;
        private CameraState _state;

        #region Events
        public static event Action<CameraState> OnCameraStateChanged;
        #endregion

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
            _animator = GetComponent<Animator>();
            _aiming = FindObjectOfType<Aiming>();
        }

        void Start()
        {
            CursorMode.DisableCursor();
            UpdateCameraState(CameraState.FollowCamera);
        }

        public void UpdateCameraState(CameraState cameraState)
        {
            if (_state == cameraState) { return; }
            _state = cameraState;
            
            switch (_state)
            {
                case CameraState.FollowCamera:
                    if (_animator == null) { return; }

                    _animator.Play("Follow Camera"); // Switch state driven camera to use Follow Camera
                    _followCameraZoom.ResetZoom();
                    _aiming.enabled = false;
                    break;
                case CameraState.AimCamera:
                    if (_animator == null) { return; }
                    
                    _animator.Play("Aim Camera"); // Switch state driven camera to use Aim Camera
                    _aiming.enabled = true;
                    break;
            }

            OnCameraStateChanged?.Invoke(_state);
        }
    
        public void ToggleAimAction(InputAction.CallbackContext context)
        {
            if (!context.performed) { return; }

            UpdateCameraState(_state == CameraState.FollowCamera ? CameraState.AimCamera : CameraState.FollowCamera);
        }
    }

    public enum CameraState
    {
        FollowCamera,
        AimCamera
    };
}