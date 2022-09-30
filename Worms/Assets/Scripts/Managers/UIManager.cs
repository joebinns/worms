using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private static Reticle reticle;
    private static Portraits portraits;

    private void Awake()
    {
        reticle = FindObjectOfType<Reticle>();
        portraits = FindObjectOfType<Portraits>();

        DisableReticle();
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerChanged += SwitchActivePortrait;
        PlayerManager.OnPlayerRemoved += DisablePortrait;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerChanged -= SwitchActivePortrait;
        PlayerManager.OnPlayerRemoved -= DisablePortrait;
    }

    public static void EnableReticle()
    {
        reticle.gameObject.SetActive(true);
    }
    
    public static void DisableReticle()
    {
        reticle.gameObject.SetActive(false);
    }

    public static void ToggleReticleZoom(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            return;
        }

        reticle.ToggleZoom(); // started or canceled
    }

    public static void SwitchActivePortrait(Player player)
    {
        portraits.SwitchActive(player.id);
    }

    public static void DisablePortrait(Player player)
    {
        portraits.DisablePortrait(player.id);
    }

}
