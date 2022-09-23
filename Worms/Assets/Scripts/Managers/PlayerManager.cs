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
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        GetPlayers();
        SortPlayersByID();

        currentPlayer = players[0];
        OnPlayerChanged?.Invoke(currentPlayer);
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

    public static void SetCurrentPlayer(int index)
    {
        // Reset old players movement inputs
        currentPlayer.GetComponent<PhysicsBasedCharacterController>().MakeInputsNull();
        
        currentPlayer = players[index];
        
        OnPlayerChanged?.Invoke(currentPlayer);
    }

    public static void FinaliseNumberOfPlayers(int desiredNumberPlayers)
    {
        for (var playerToRemove = MAX_PLAYERS - 1; playerToRemove >= desiredNumberPlayers; playerToRemove--)
        {
            var player = players[playerToRemove];
            players.Remove(player);
            Destroy(player.gameObject);
        }

        PlayerManager.SetCurrentPlayer(0);
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

    private void OnDestroy()
    {
        foreach (Player player in players)
        {
            Destroy(player.gameObject);
        }
    }

}

