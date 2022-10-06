using System;
using System.Collections.Generic;
using Player.Physics_Based_Character_Controller;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        public List<global::Player.Player> players { get; set; } = new List<global::Player.Player>();
        public int numPlayers => players.Count;
        public global::Player.Player currentPlayer { get; private set; }
        public event Action<global::Player.Player> OnPlayerChanged;
        public event Action<global::Player.Player> OnPlayerRemoved;
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
            players = new List<global::Player.Player>();
            foreach (global::Player.Player player in FindObjectsOfType<global::Player.Player>())
            {
                players.Add(player.GetComponent<global::Player.Player>());
            }
        }

        private void SortPlayersByID()
        {
            players.Sort((x, y) => x.id.CompareTo(y.id));
        }
    
        public global::Player.Player SetCurrentPlayer(int index)
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
                player.shouldSpawn = false;
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

        private void SetAllLookDirectionOptions(PhysicsBasedCharacterController.LookDirectionOptions option)
        {
            foreach (Player player in players)
            {
                player.SetLookDirectionOption(option);
            }
        }

        public void DeletePlayer(Player player)
        {
            if (numPlayers == 1) // To ensure there is always 1 player alive
            {
                return;
            }

            player.shouldSpawn = false;

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
}

