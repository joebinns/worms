using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portraits : MonoBehaviour
{
    [SerializeField] private Image _activePortrait;

    private float _minPortraitSize = 35f;
    private float _maxPortraitSize = 40f;

    private float _portraitSpacing = 50f; // Vertical distance between the centres of portraits

    public GameObject emptyPortraitPrefab;

    private List<Image> _portraits = new List<Image>();

    private void Start()
    {
        LoadPortraits();

        _activePortrait = _portraits[0];
        var activeColor = _activePortrait.color;
        activeColor.a = 1f;
        _activePortrait.color = activeColor;
        StartCoroutine(EasedLerpScale(_activePortrait.GetComponent<RectTransform>(), true));
    }

    public void LoadPortraits()
    {
        var totalSpacing = _portraitSpacing * (PlayerManager.numPlayers - 1);

        var startPosition = totalSpacing / 2;

        var position = startPosition;

        foreach (Player player in PlayerManager.players)
        {
            // Instantiate portrait prefabs with calculated vertical displacements
            var portrait = Instantiate(emptyPortraitPrefab);
            portrait.transform.SetParent(gameObject.transform, false);

            portrait.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, position);
            position -= _portraitSpacing;

            portrait.GetComponent<Image>().sprite = player.portrait;
            _portraits.Add(portrait.GetComponent<Image>());
        }
    }

    public void SwitchActive(int id)
    {
        // Deactivate old portrait
        var activeColor = _activePortrait.color;
        activeColor.a = 0.25f;
        _activePortrait.color = activeColor;
        StartCoroutine(EasedLerpScale(_activePortrait.GetComponent<RectTransform>(), false));

        // Activate new portrait
        _activePortrait = _portraits[id];
        activeColor = _activePortrait.color;
        activeColor.a = 1f;
        _activePortrait.color = activeColor;
        StartCoroutine(EasedLerpScale(_activePortrait.GetComponent<RectTransform>(), true));
    }

    
    /*
    public void DisablePortrait(int id) // This id doesn't work the same as PlayerManager id... // Should I change this to take in a Player?
    {
        
        Debug.Log(id);
        Debug.Log(PlayerManager.numPlayers);

        if (id >= PlayerManager.numPlayers) // This doesn't work
        {
            return;
        }
        
        
        //var portraitToDisable = _portraits[id];
        //var disabledColor = Color.black;
        //disabledColor.a = 0.25f;
        //portraitToDisable.color = disabledColor;
        //StartCoroutine(EasedLerpScale(portraitToDisable.GetComponent<RectTransform>(), false));
        
        var portraitToDisable = _portraits[id];
        //var player = PlayerManager.players[id];
        var player = PlayerManager.players[PlayerManager.IdToIndex(id)];
        portraitToDisable.sprite = player.deadPortrait;
        

    }
    */
    

    public void DisablePortrait(Player player)
    {
        // Find portrait using player id
        var maxIndex = _portraits.Count - 1;
        if (player.id > maxIndex)
        {
            // If player doesn't have a portrait, return
            return;
        }

        var portrait = _portraits[player.id];

        // Disable portrait
        portrait.sprite = player.deadPortrait;

    }
    
    

    private IEnumerator EasedLerpScale(RectTransform portrait, bool shouldEnlarge)
    {
        var t = 0f;
        var easedT = 0f;
        while (Mathf.Abs(t) < 1f)
        {
            easedT = Easing.Back.Out(t);
            var size = (shouldEnlarge ? _minPortraitSize : _maxPortraitSize) + easedT * (_maxPortraitSize - _minPortraitSize) * (shouldEnlarge ? 1 : -1);
            portrait.sizeDelta = Vector2.one * size;
            yield return new WaitForEndOfFrame(); // Surely this is poor practise.
            t += Time.deltaTime; // Is this the correct deltaTime for a IEnumerator?
        }
        yield break;
    }
}

