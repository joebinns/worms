using System;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class PlayerSpawner : MonoBehaviour
    {
        #region Spawn Points
        [SerializeField] private List<Transform> _spawnPoints;
        private List<Transform> _availableSpawnPoints;
        #endregion
        
        #region Events
        public static event Action OnPlayersSpawned;
        #endregion

        private void Awake()
        {
            _availableSpawnPoints = _spawnPoints;
        }

        private void Start()
        {
            SpawnPlayers();
        }

        private void SpawnPlayers()
        {
            var players = new List<Player>(PlayerManager.Instance.Players);
            foreach (Player player in players)
            {
                if (player.shouldSpawn == false)
                {
                    PlayerManager.Instance.DeletePlayer(player);
                    continue;
                }
                var spawnPoint = GetRandomSpawnPoint();
                // Position the player to the randomly selected spawnPoint
                player.transform.position = spawnPoint.transform.position;
            }
            OnPlayersSpawned?.Invoke();
        }

        private Transform GetRandomSpawnPoint()
        {
            // Get a random available spawnPoint
            var randomIndex = UnityEngine.Random.Range(0, _availableSpawnPoints.Count);
            var spawnPoint = _availableSpawnPoints[randomIndex];
            _availableSpawnPoints.RemoveAt(randomIndex);
            return spawnPoint;
        }
    }
}
