using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    private List<Transform> _availableSpawnPoints = new List<Transform>();

    [SerializeField] private List<GameObject> _playerPrefabs;

    private void Awake()
    {
        _availableSpawnPoints = _spawnPoints;
        
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        foreach (Player player in PlayerManager.players)
        {
            Debug.Log(player.name);
        }
        
    }
    
}
