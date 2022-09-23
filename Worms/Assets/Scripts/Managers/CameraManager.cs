using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    
    private static Animator animator;

    private static Aiming aiming;

    public static CameraState state;

    public static CameraZoom followCameraZoom;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        aiming = FindObjectOfType<Aiming>();
        followCameraZoom = GameObject.Find("Follow Camera").GetComponent<CameraZoom>();
    }
    
    void Start()
    {
        UpdateCameraState(CameraState.FollowCamera);
    }

    public static void UpdateCameraState(CameraState cameraState)
    {
        state = cameraState;
        
        switch (state)
        {
            case CameraState.FollowCamera:
                animator.Play("Follow Camera"); // Switch state driven camera to use Follow Camera
                InputManager.SwitchActionMap("Moving");
                followCameraZoom.ResetZoom();
                aiming.enabled = false;
                UIManager.DisableReticle();
                CursorVisibilityToggle.DisableCursor();
                break;
            case CameraState.AimCamera:
                animator.Play("Aim Camera"); // Switch state driven camera to use Aim Camera
                InputManager.SwitchActionMap("Aiming");
                aiming.enabled = true;
                UIManager.EnableReticle();
                CursorVisibilityToggle.EnableCursor();
                break;
        }
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

