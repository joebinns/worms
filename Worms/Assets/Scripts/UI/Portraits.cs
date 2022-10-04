using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portraits : UIRack
{
    private float _portraitSpacing = 50f; // Vertical distance between the centres of portraits
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
        var totalSpacing = _portraitSpacing * (PlayerManager.numPlayers - 1);

        var startPosition = totalSpacing / 2;

        var position = startPosition;

        foreach (Player player in PlayerManager.players)
        {
            // Instantiate portrait prefabs with calculated vertical displacements
            var portrait = Instantiate(_emptyPortraitPrefab);
            portrait.transform.SetParent(gameObject.transform, false);

            portrait.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, position);
            position -= _portraitSpacing;

            portrait.GetComponent<Image>().sprite = player.portrait;
            images.Add(portrait.GetComponent<Image>());
        }

        ActivateImage(0);
        
    }

    public void DisablePortrait(Player player)
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
        portrait.sprite = player.deadPortrait;

    }
}

