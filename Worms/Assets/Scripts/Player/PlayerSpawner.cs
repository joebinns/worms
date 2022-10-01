using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    private List<Transform> _availableSpawnPoints = new List<Transform>();

    private void Start()
    {
        _availableSpawnPoints = _spawnPoints;
        
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        var players = new List<Player>(PlayerManager.players);
        foreach (Player player in players)
        {
            if (player.playerSettings.shouldSpawn == false)
            {
                PlayerManager.DeletePlayer(player);
                continue;
            }
            
            // Get a random available spawnPoint
            var random = UnityEngine.Random.Range(0, _availableSpawnPoints.Count);
            var spawnpoint = _availableSpawnPoints[random];
            _availableSpawnPoints.RemoveAt(random);

            // Position the player to spawnPoint
            player.transform.position = spawnpoint.transform.position;

        }
        
    }
    
}
