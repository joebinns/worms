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

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        aiming = FindObjectOfType<Aiming>();
    }
    
    void Start()
    {
        state = CameraState.FollowCamera;
        //UpdateCameraState(CameraState.FollowCamera);
    }

    public static void UpdateCameraState(CameraState cameraState)
    {
        state = cameraState;
        
        switch (state)
        {
            case CameraState.FollowCamera:
                animator.Play("Follow Camera");
                // Enable Movement
                PlayerManager.EnableCurrentPlayerMovement();
                // Disable Aiming
                aiming.enabled = false;
                // Turn off crosshair
                //UIManager.DisableReticle();
                break;
            case CameraState.AimCamera:
                animator.Play("Aim Camera");
                // Disable Movement
                PlayerManager.DisablePlayerMovement();
                // Enable Aiming
                aiming.enabled = true;
                // Turn on crosshair
                //UIManager.EnableReticle();
                break;
        }
    }
    
    public void ToggleAimAction(InputAction.CallbackContext context)
    {
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

