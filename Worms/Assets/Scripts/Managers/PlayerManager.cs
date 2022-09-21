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
        
        foreach (Player player in FindObjectsOfType<Player>())
        {
            _players.Add(player.GetComponent<Player>());
        }

        _players.Sort((x, y) => x.id.CompareTo(y.id)); // Sort objects of type Player by id (ascending.)

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

