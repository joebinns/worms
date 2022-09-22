using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelection : MonoBehaviour
{
    private float _defaultRideHeight = 2f;
    private float _increasedRideHeight = 2.5f;

    private Player _previousPlayer;

    private void OnEnable()
    {
        PlayerManager.OnPlayerChanged += ChangePlayerSelection;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerChanged -= ChangePlayerSelection;
    }

    private void Start()
    {
        _previousPlayer = PlayerManager.currentPlayer;
    }

    private void ChangePlayerSelection(Player player)
    {
        AdjustRideHeights(player);
        AdjustMaterials(player);

        _previousPlayer = player;
    }

    private void AdjustRideHeights(Player player)
    {
        _previousPlayer.GetComponent<PhysicsBasedCharacterController>()._rideHeight = _defaultRideHeight;
        player.GetComponent<PhysicsBasedCharacterController>()._rideHeight = _increasedRideHeight;
        
    }

    private void AdjustMaterials(Player player)
    {
        if (_previousPlayer.id > player.id)
        {
            _previousPlayer.jumpsuit.GetComponent<Renderer>().material = _previousPlayer.ditherMaterial;
            _previousPlayer.visor.GetComponent<Renderer>().material = _previousPlayer.ditherMaterial;
        }
        player.jumpsuit.GetComponent<Renderer>().material = player.jumpsuitMaterial;
        player.visor.GetComponent<Renderer>().material = player.visorMaterial;
    }

    public static void NextPlayer()
    {
        if (PlayerManager.currentPlayer.id < 3)
        {
            PlayerManager.SetCurrentPlayer((PlayerManager.currentPlayer.id + 1));
        } 

    }

    public static void PreviousPlayer()
    {
        if (PlayerManager.currentPlayer.id > 0)
        {
            PlayerManager.SetCurrentPlayer((PlayerManager.currentPlayer.id - 1));
        }

    }

    public void FinaliseSelection()
    {
        PlayerManager.FinaliseNumberOfPlayers(PlayerManager.currentPlayer.id + 1);
        foreach (Player player in PlayerManager.players)
        {
            player.GetComponent<PhysicsBasedCharacterController>()._rideHeight = _defaultRideHeight;
            player.GetComponent<PhysicsBasedCharacterController>()._characterLookDirection = PhysicsBasedCharacterController.lookDirectionOptions.velocity;
            
            player.jumpsuit.GetComponent<Renderer>().material = player.jumpsuitMaterial;
            player.visor.GetComponent<Renderer>().material = player.visorMaterial;
        }
        this.GetComponent<PlayerSelection>().enabled = false;
    }
}
