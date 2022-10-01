using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRack : MonoBehaviour
{
    public Image activePortrait;

    public float minPortraitSize = 35f;
    public float maxPortraitSize = 40f;

    public List<Image> portraits = new List<Image>();

    public void SwitchActive(int id)
    {
        // Deactivate old portrait
        var activeColor = activePortrait.color;
        activeColor.a = 0.25f;
        activePortrait.color = activeColor;
        StartCoroutine(EasedLerpScale(activePortrait.GetComponent<RectTransform>(), false));

        // Activate new portrait
        activePortrait = portraits[id];
        activeColor = activePortrait.color;
        activeColor.a = 1f;
        activePortrait.color = activeColor;
        StartCoroutine(EasedLerpScale(activePortrait.GetComponent<RectTransform>(), true));
    }
    
    public IEnumerator EasedLerpScale(RectTransform portrait, bool shouldEnlarge)
    {
        var t = 0f;
        var easedT = 0f;
        while (Mathf.Abs(t) < 1f)
        {
            easedT = Easing.Back.Out(t);
            var size = (shouldEnlarge ? minPortraitSize : maxPortraitSize) + easedT * (maxPortraitSize - minPortraitSize) * (shouldEnlarge ? 1 : -1);
            portrait.sizeDelta = Vector2.one * size;
            yield return new WaitForEndOfFrame(); // Surely this is poor practise.
            t += Time.deltaTime; // Is this the correct deltaTime for a IEnumerator?
        }
        yield break;
    }
}

