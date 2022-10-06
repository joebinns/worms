using Players;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Portraits : UIRack
    {
        [SerializeField] private GameObject _emptyPortraitPrefab;

        #region Constants
        private const float PORTRAIT_SPACING = 50f; // Vertical distance between the centres of portraits
        #endregion

        private void OnEnable()
        {
            PlayerSpawner.OnPlayersSpawned += LoadPortraits;
        }

        private void OnDisable()
        {
            PlayerSpawner.OnPlayersSpawned -= LoadPortraits;
        }

        public void LoadPortraits()
        {
            var totalSpacing = PORTRAIT_SPACING * (PlayerManager.Instance.NumPlayers - 1);
            var startPosition = totalSpacing / 2;
            var position = startPosition;
            foreach (Player player in PlayerManager.Instance.Players)
            {
                LoadPortrait(player, position);
                position -= PORTRAIT_SPACING;
            }
            ActivateImage(0);
        }

        private void LoadPortrait(Player player, float position)
        {
            // Instantiate portrait prefabs with calculated vertical displacements
            var portrait = Instantiate(_emptyPortraitPrefab, gameObject.transform, false);
            portrait.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, position);
            portrait.GetComponent<Image>().sprite = player.Portrait;
            _images.Add(portrait.GetComponent<Image>());
        }

        public void DisablePortrait(Players.Player player)
        {
            // Find portrait using player id
            var maxIndex = _images.Count - 1;
            if (player.id > maxIndex)
            {
                // If player doesn't have a portrait, return
                return;
            }
            var portrait = _images[player.id];
            // Disable portrait
            portrait.sprite = player.DeadPortrait;

        }
    }
}

