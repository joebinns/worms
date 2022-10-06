using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float _defaultZoom = 12f;
        [SerializeField] private Vector2 _zoomRange = new Vector2(4.5f, 30f);
        [SerializeField] private float _zoomSensitivity = 1.5f;
        
        private CinemachineVirtualCamera _virtualCamera;
        private float _currentZoom;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void ResetZoom()
        {
            _currentZoom = _defaultZoom;
            RefreshZoom();
        }

        private void AdjustZoom(float deltaZoom)
        {
            _currentZoom -= deltaZoom;
            _currentZoom = Mathf.Clamp(_currentZoom, _zoomRange.x, _zoomRange.y);
            RefreshZoom();
        }

        private void RefreshZoom()
        {
            var componentBase = _virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = _currentZoom;
            }
            else if (componentBase is Cinemachine3rdPersonFollow)
            {
                (componentBase as Cinemachine3rdPersonFollow).CameraDistance = _currentZoom;
            }
        }

        public void ShiftInput(InputAction.CallbackContext context)
        {
            if (context.performed) { return; }
            else if (context.started)
            {
                AdjustZoom(_zoomSensitivity);
            }
            else if (context.canceled)
            {
                AdjustZoom(-_zoomSensitivity);
            }
        }
    
        public void ScrollWheelInput(InputAction.CallbackContext context)
        {   
            if (!context.performed) { return; }

            float scrollDirection = context.ReadValue<float>();
            if (scrollDirection > 0) // Scroll up
            {
                AdjustZoom(_zoomSensitivity);
            }
            else if (scrollDirection < 0) // Scroll down
            {
                AdjustZoom(-_zoomSensitivity);
            }
        }
    }
}
