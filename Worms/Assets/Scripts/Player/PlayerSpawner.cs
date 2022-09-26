using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    private List<Transform> _availableSpawnPoints = new List<Transform>();

    //[SerializeField] private List<GameObject> _playerPrefabs;

    private void Start()
    {
        _availableSpawnPoints = _spawnPoints;
        
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        foreach (Player player in PlayerManager.players)
        {
            // Get a random available spawnPoint
            var random = UnityEngine.Random.Range(0, _availableSpawnPoints.Count);
            var spawnpoint = _availableSpawnPoints[random];
            _availableSpawnPoints.RemoveAt(random);

            // Position the player to spawnPoint
            player.transform.position = spawnpoint.transform.position;

        }
        
    }
    
}
