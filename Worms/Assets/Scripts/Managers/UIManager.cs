using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private static Reticle reticle;
    private static Portraits portraits;
    private static UIRack hotbar;
    private static CountdownTimer turnTimer;
    private static Ammunition ammunition;

    private void Awake()
    {
        reticle = FindObjectOfType<Reticle>();
        portraits = FindObjectOfType<Portraits>();
        hotbar = FindObjectOfType<UIRack>(); // May this also find Portraits accidentally, due to inheritance?
        turnTimer = FindObjectOfType<CountdownTimer>();
        ammunition = FindObjectOfType<Ammunition>();

        DisableReticle();
    }

    private void Start()
    {
        HideHotbar();
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerChanged += SwitchActivePortrait;
        PlayerManager.OnPlayerRemoved += DisablePortrait;
        PlayerManager.OnLastPlayerStanding += NextScene;
        PlayerManager.OnPlayerChanged += ResetTurnTimer;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerChanged -= SwitchActivePortrait;
        PlayerManager.OnPlayerRemoved -= DisablePortrait;
        PlayerManager.OnLastPlayerStanding -= NextScene;
        PlayerManager.OnPlayerChanged -= ResetTurnTimer;
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

    public static void SwitchActiveHotbar(int id)
    {
        hotbar.SwitchActive(id);
        // Update ammo
    }

    public static void SwitchActivePortrait(Player player)
    {
        portraits.SwitchActive(player.id);
    }

    public static void DisablePortrait(Player player)
    {
        portraits.DisablePortrait(player);
    }

    public void ResetTurnTimer(Player player)
    {
        turnTimer.ResetTimer();
    }

    public static void RefreshAmmunition() // Called on weapon switch and on attack
    {
        ammunition.RefreshDisplay();
    }

    public static void HideHotbar()
    {
        hotbar.gameObject.SetActive(false);
        HideAmmunition();
    }

    public static void ShowHotbar()
    {
        hotbar.gameObject.SetActive(true);
    }

    public static void HideAmmunition()
    {
        ammunition.gameObject.SetActive(false);
    }
    
    public static void ShowAmmunition()
    {
        ammunition.gameObject.SetActive(true);
    }

    public void NextScene()
    {
        StartCoroutine(LoadingScreen.ChangeSceneImpatient(SceneIndices.VICTORY));
    }

}
