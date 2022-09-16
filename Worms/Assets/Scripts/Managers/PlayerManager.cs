using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private static List<Player> _players = new List<Player>();
    
    public static int numPlayers => _players.Count;

    public static Player currentPlayer { get; private set; }
    
    public static event Action<GameObject> OnPlayerChanged;

    private void Start()
    {
        foreach (Player player in FindObjectsOfType<Player>())
        {
            _players.Add(player.GetComponent<Player>());
        }

        currentPlayer = _players[0];
    }

    public static void SetCurrentPlayer(int index)
    {
        // Disable old Player movement (Turn off input)
        DisablePlayerMovement();

        currentPlayer = _players[index];
        
        // Enable new Player movement (Turn on input)
        EnableCurrentPlayerMovement();
        
        OnPlayerChanged?.Invoke(currentPlayer.gameObject);
        
    }

    public static void DisablePlayerMovement()
    {
        foreach (Player player in _players)
        {
            currentPlayer.GetComponent<PlayerInput>().enabled = false;
        }
    }

    public static void EnableCurrentPlayerMovement()
    {
        currentPlayer.GetComponent<PlayerInput>().enabled = true;
    }

}

