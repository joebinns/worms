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
    
    public static event Action<GameObject> OnPlayerChanged;

    private void Awake()
    {
        Instance = this;
        
        foreach (Player player in FindObjectsOfType<Player>())
        {
            _players.Add(player.GetComponent<Player>());
        }

        currentPlayer = _players[0];
            
            
        //GameObject.FindObjectOfType<InputManager>().SubscribeToMovement(PlayerManager.currentPlayer);
        //GameObject.FindObjectOfType<InputManager>().SubscribeToTurns();
    }

    public static void SetCurrentPlayer(int index)
    {
        // Reset old players movement inputs
        currentPlayer.GetComponent<PhysicsBasedCharacterController>().MakeInputsNull();
        
        // Unsubscribe old player
        InputManager.UnsubscribeFromMovement(currentPlayer);

        currentPlayer = _players[index];
        
        // Subscribe new player
        InputManager.SubscribeToMovement(currentPlayer);
        
        OnPlayerChanged?.Invoke(currentPlayer.gameObject);
        
    }
    
    
    

}

