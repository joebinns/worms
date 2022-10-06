using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Portraits : UIRack
    {
        private const float PORTRAIT_SPACING = 50f; // Vertical distance between the centres of portraits
        [SerializeField] private GameObject _emptyPortraitPrefab;

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

            foreach (Player.Player player in PlayerManager.Instance.Players)
            {
                // Instantiate portrait prefabs with calculated vertical displacements
                var portrait = Instantiate(_emptyPortraitPrefab, gameObject.transform, false);

                portrait.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, position);
                position -= PORTRAIT_SPACING;

                portrait.GetComponent<Image>().sprite = player.Portrait;
                images.Add(portrait.GetComponent<Image>());
            }

            ActivateImage(0);
        
        }

        public void DisablePortrait(Player.Player player)
        {
            // Find portrait using player id
            var maxIndex = images.Count - 1;
            if (player.id > maxIndex)
            {
                // If player doesn't have a portrait, return
                return;
            }

            var portrait = images[player.id];

            // Disable portrait
            portrait.sprite = player.DeadPortrait;

        }
    }
}

