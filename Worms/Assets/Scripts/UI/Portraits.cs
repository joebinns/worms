using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portraits : MonoBehaviour
{
    [SerializeField] private Image _activePortrait;

    public void SwitchActive(Image portrait)
    {
        // Deactivate old portrait
        var activeColor = _activePortrait.color;
        activeColor.a = 0.25f;
        _activePortrait.color = activeColor;

        // Activate new portrait
        _activePortrait = portrait;
        activeColor = _activePortrait.color;
        activeColor.a = 1f;
        _activePortrait.color = activeColor;
    }
}

