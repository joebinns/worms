using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        UpdateGameState(GameState.PlayersTurns);
    }

    public void UpdateGameState(GameState gameState)
    {
        state = gameState;
        
        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.PlayersSelect:
                break;
            case GameState.PlayersSpawn:
                break;
            case GameState.PlayersTurns:
                HandlePlayersTurns();
                break;
            case GameState.PauseMenu:
                break;
            case GameState.GameOver:
                break;
        }

        OnGameStateChanged?.Invoke(state); // '?.Invoke' prevent a Null error exception, in the case that there are no subscribers 
    }

    private void HandlePlayersTurns()
    {
        
    }
}

public enum GameState
{
    MainMenu,
    PlayersSelect,
    PlayersSpawn,
    PlayersTurns,
    PauseMenu,
    GameOver
}
