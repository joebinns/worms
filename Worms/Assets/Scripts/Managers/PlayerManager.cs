using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public static List<Player> players { get; private set; } = new List<Player>();
    public static int numPlayers => players.Count;
    public static Player currentPlayer { get; private set; }
    public static event Action<Player> OnPlayerChanged;

    private const int MAX_PLAYERS = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("oops");
            Destroy(this);
            return;
        }

        GetPlayers();
        SortPlayersByID();

        //currentPlayer = players[0];
        //OnPlayerChanged?.Invoke(currentPlayer);
        SetCurrentPlayer(0);
    }

    private void GetPlayers()
    {
        players = new List<Player>();
        foreach (Player player in FindObjectsOfType<Player>())
        {
            players.Add(player.GetComponent<Player>());
        }
    }

    private void SortPlayersByID()
    {
        players.Sort((x, y) => x.id.CompareTo(y.id));
    }
    
    public static Player SetCurrentPlayer(int index)
    {
        if (currentPlayer != null)
        {
            // Reset old players movement inputs
            currentPlayer.GetComponent<PhysicsBasedCharacterController>().MakeInputsNull();
        }

        currentPlayer = players[index];
        
        OnPlayerChanged?.Invoke(currentPlayer);

        return (currentPlayer);
    }

    public static void FinaliseNumberOfPlayers(int desiredNumberPlayers) // This should probably be moved to PlayerSelection
    {
        for (var playerToRemove = MAX_PLAYERS - 1; playerToRemove >= desiredNumberPlayers; playerToRemove--)
        {
            var player = players[playerToRemove];
            //players.Remove(player);
            //Destroy(player.gameObject);
            
            // Set a bool in the Player's playerSettings
            player.playerSettings.shouldSpawn = false;
        }

        //PlayerManager.SetCurrentPlayer(0);
    }

    private void EnableAllParticleSystems()
    {
        foreach (Player player in players)
        {
            player.EnableParticleSystem();
        }
    }

    private void DisableAllParticleSystems()
    {
        foreach (Player player in players)
        {
            player.DisableParticleSystem();
        }
    }

    private void SetAllLookDirectionOptions(PhysicsBasedCharacterController.lookDirectionOptions option)
    {
        foreach (Player player in players)
        {
            player.SetLookDirectionOption(option);
        }
    }

    public static void DeletePlayer(Player player)
    {
        players.Remove(player);
        Destroy(player.gameObject);
    }

    private void OnDestroy()
    {
        /*
        foreach (Player player in players)
        {
            Destroy(player.gameObject);
        }
        */
    }

}

