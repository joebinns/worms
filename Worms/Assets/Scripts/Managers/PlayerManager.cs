using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public static List<Player> players { get; private set; } = new List<Player>();

    public static int numPlayers => players.Count;

    public static Player currentPlayer { get; private set; }
    
    public static event Action<Player> OnPlayerChanged;

    private static int maxPlayers = 4;

    private void Awake()
    {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Player player in FindObjectsOfType<Player>())
        {
            players.Add(player.GetComponent<Player>());
        }

        players.Sort((x, y) => x.id.CompareTo(y.id)); // Sort objects of type Player by id (ascending.)

        currentPlayer = players[0];
        OnPlayerChanged?.Invoke(currentPlayer);
    }

    public static void SetCurrentPlayer(int index)
    {
        // Reset old players movement inputs
        currentPlayer.GetComponent<PhysicsBasedCharacterController>().MakeInputsNull();
        
        currentPlayer = players[index];
        
        OnPlayerChanged?.Invoke(currentPlayer);
        
    }

    public static void FinaliseNumberOfPlayers(int desiredNumberPlayers)
    {
        for (var playerToRemove = maxPlayers - 1; playerToRemove >= desiredNumberPlayers; playerToRemove--)
        {
            Debug.Log("Remove player");
            Debug.Log(playerToRemove);
            players.RemoveAt(playerToRemove);
        }

        PlayerManager.SetCurrentPlayer(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

}

