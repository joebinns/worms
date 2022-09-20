using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private float _defaultZoom;

    private float _currentZoom;

    [SerializeField] private Vector2 _zoomRange = new Vector2(4.5f, 30f);

    [SerializeField] private float _zoomSensitivity = 1.5f;
    
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
        CinemachineComponentBase componentBase = _virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
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
        if (context.performed)
        {
            return;
        }

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
        if (!context.performed)
        {
            return;
        }

        float z = context.ReadValue<float>();
        
        if (z > 0) // Scroll up
        {
            AdjustZoom(_zoomSensitivity);
        }
        else if (z < 0) // Scroll down
        {
            AdjustZoom(-_zoomSensitivity);
        }

    }
}
