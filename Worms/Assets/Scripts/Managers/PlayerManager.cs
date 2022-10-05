using System;
using System.Collections;
using System.Collections.Generic;
using Player.Physics_Based_Character_Controller;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public List<Player.Player> players { get; set; } = new List<Player.Player>();
    public int numPlayers => players.Count;
    public Player.Player currentPlayer { get; private set; }
    public event Action<Player.Player> OnPlayerChanged;
    public event Action<Player.Player> OnPlayerRemoved;
    public event Action OnLastPlayerStanding;

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

    public int IdToIndex(int id) // Since the list size is subject to change, meaning indices are inconsistent-
    {
        var index = Instance.players.FindIndex(x => x.id == id);
        return index;
    }

    private void GetPlayers()
    {
        players = new List<Player.Player>();
        foreach (Player.Player player in FindObjectsOfType<Player.Player>())
        {
            players.Add(player.GetComponent<Player.Player>());
        }
    }

    private void SortPlayersByID()
    {
        players.Sort((x, y) => x.id.CompareTo(y.id));
    }
    
    public Player.Player SetCurrentPlayer(int index)
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

    public void FinaliseNumberOfPlayers(int desiredNumberPlayers) // This should probably be moved to PlayerSelection
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
        foreach (Player.Player player in players)
        {
            player.EnableParticleSystem();
        }
    }

    private void DisableAllParticleSystems()
    {
        foreach (Player.Player player in players)
        {
            player.DisableParticleSystem();
        }
    }

    private void SetAllLookDirectionOptions(PhysicsBasedCharacterController.LookDirectionOptions option)
    {
        foreach (Player.Player player in players)
        {
            player.SetLookDirectionOption(option);
        }
    }

    public void DeletePlayer(Player.Player player)
    {
        if (numPlayers == 1) // To ensure there is always 1 player alive
        {
            return;
        }

        player.playerSettings.shouldSpawn = false;

        OnPlayerRemoved?.Invoke(player); // Disable portrait

        players.Remove(player); // Remove player from list
        Destroy(player.gameObject); // Destroy player game object

        if (player == currentPlayer)
        {
            TurnManager.NextTurn();
        }

        if (numPlayers == 1) // If this is the last player alive
        {
            OnLastPlayerStanding?.Invoke();
        }

    }
}

