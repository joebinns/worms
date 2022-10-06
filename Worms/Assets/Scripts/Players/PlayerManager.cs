using System;
using System.Collections.Generic;
using Players.Physics_Based_Character_Controller;
using UnityEngine;

namespace Players
{
    public class PlayerManager : MonoBehaviour
    {
        #region Singleton
        public static PlayerManager Instance;
        #endregion

        #region Players
        public List<Player> Players { get; private set; }
        public int NumPlayers => Players.Count;
        public Player CurrentPlayer { get; private set; }
        private const int MAX_PLAYERS = 4;
        #endregion

        #region Events
        public event Action<Player> OnCurrentPlayerChanged;
        public event Action<Player> OnPlayerRemoved;
        public event Action OnLastPlayerStanding;
        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            GetPlayers();
            SortPlayersByID();

            CurrentPlayer = Players[0];
        }

        // Converts player id to index. Since the list size is subject to change, indices are inconsistent
        public int IdToIndex(int id) 
        {
            var index = Instance.Players.FindIndex(x => x.id == id);
            return index;
        }

        private void GetPlayers()
        {
            Players = new List<Player>();
            foreach (Player player in FindObjectsOfType<Player>())
            {
                Players.Add(player.GetComponent<Player>());
            }
        }

        // Sort Players list by player ids, in ascending order.
        private void SortPlayersByID()
        {
            Players.Sort((x, y) => x.id.CompareTo(y.id));
        }
    
        public Player SetCurrentPlayer(int index)
        {
            if (CurrentPlayer != null)
            {
                // Reset old players movement inputs
                CurrentPlayer.GetComponent<PhysicsBasedCharacterController>().MakeInputsNull();
            }

            CurrentPlayer = Players[index];   
        
            OnCurrentPlayerChanged?.Invoke(CurrentPlayer);

            return (CurrentPlayer);
        }

        public void FinaliseNumberOfPlayers(int desiredNumberPlayers) // This should probably be moved to PlayerSelection
        {
            for (var playerToRemove = MAX_PLAYERS - 1; playerToRemove >= desiredNumberPlayers; playerToRemove--)
            {
                Players[playerToRemove].shouldSpawn = false;
            }
        }

        public void DeletePlayer(Player player)
        {
            if (NumPlayers == 1) // To ensure there is always 1 player alive
            {
                return;
            }

            player.shouldSpawn = false;

            OnPlayerRemoved?.Invoke(player); // Disable portrait

            Players.Remove(player); // Remove player from list
            Destroy(player.gameObject); // Destroy player game object

            if (player == CurrentPlayer)
            {
                TurnManager.NextTurn();
            }

            if (NumPlayers == 1) // If this is the last player alive
            {
                OnLastPlayerStanding?.Invoke();
            }

        }
    }
}

