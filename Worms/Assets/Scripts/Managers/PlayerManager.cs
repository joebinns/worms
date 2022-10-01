using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public static List<Player> players { get; private set; } = new List<Player>();
    public static int numPlayers => players.Count;
    public static Player currentPlayer { get; private set; }
    public static event Action<Player> OnPlayerChanged;
    public static event Action<Player> OnPlayerRemoved;
    public static event Action OnLastPlayerStanding;

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

        currentPlayer = players[0];
    }

    public static int IdToIndex(int id) // Since the list size is subject to change, meaning indices are inconsistent-
    {
        var index = players.FindIndex(x => x.id == id);
        return index;
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

            // Un-equip any weapons
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
        if (numPlayers == 1)
        {
            return;
        }

        player.playerSettings.shouldSpawn = false;

        players.Remove(player);
        Destroy(player.gameObject);

        OnPlayerRemoved?.Invoke(player);

        if (player == currentPlayer)
        {
            TurnManager.NextTurn();
        }

        if (numPlayers == 1)
        {
            OnLastPlayerStanding?.Invoke();
        }

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

