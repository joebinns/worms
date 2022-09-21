using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelection : MonoBehaviour
{
    private float _defaultRideHeight = 2f;
    private float _increasedRideHeight = 2.5f;

    [SerializeField] private Player _previousPlayer;

    private void Awake()
    {
        PlayerManager.OnPlayerChanged += ChangePlayerSelection;
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
}
