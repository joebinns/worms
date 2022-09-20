using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portraits : MonoBehaviour
{
    [SerializeField] private Image _activePortrait;

    private float _minPortraitSize = 35f;
    private float _maxPortraitSize = 40f;

    public void SwitchActive(Image portrait)
    {
        // Deactivate old portrait
        var activeColor = _activePortrait.color;
        activeColor.a = 0.25f;
        _activePortrait.color = activeColor;
        StartCoroutine(lerp(_activePortrait.GetComponent<RectTransform>(), false));

        // Activate new portrait
        _activePortrait = portrait;
        activeColor = _activePortrait.color;
        activeColor.a = 1f;
        _activePortrait.color = activeColor;

        StartCoroutine(lerp(_activePortrait.GetComponent<RectTransform>(), true));
    }

    private IEnumerator lerp(RectTransform portrait, bool shouldEnlarge)
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

