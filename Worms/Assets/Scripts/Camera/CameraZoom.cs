using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    private CinemachineVirtualCamera _followCamera;
    private float _currentZoom;

    private float _zoomSensitivity = 1.5f;
    
    private void Awake()
    {
        _followCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void AdjustZoom(float deltaZoom)
    {
        _currentZoom -= deltaZoom;
        _currentZoom = Mathf.Clamp(_currentZoom, 4.5f, 30f);

        CinemachineComponentBase componentBase = _followCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = _currentZoom; // your value
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
