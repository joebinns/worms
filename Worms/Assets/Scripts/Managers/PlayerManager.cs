using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    private static List<Player> _players = new List<Player>();
    
    public static int numPlayers => _players.Count;

    public static Player currentPlayer { get; private set; }
    
    public static event Action<Player> OnPlayerChanged;

    private void Awake()
    {
        Instance = this;
        
        foreach (Player player in FindObjectsOfType<Player>()) // TODO: I want the _players List to be in a specific order.
        {
            _players.Add(player.GetComponent<Player>());
        }

        currentPlayer = _players[0];
        OnPlayerChanged?.Invoke(currentPlayer);
    }

    public static void SetCurrentPlayer(int index)
    {
        // Reset old players movement inputs
        currentPlayer.GetComponent<PhysicsBasedCharacterController>().MakeInputsNull();
        
        currentPlayer = _players[index];
        
        OnPlayerChanged?.Invoke(currentPlayer);
        
    }
    
    
    

}

