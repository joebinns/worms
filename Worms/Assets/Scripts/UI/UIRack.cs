using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRack : MonoBehaviour
{
    public Image activeImage;

    public float minImageSize = 35f;
    public float maxImageSize = 40f;

    public float transitionTime = 1f;

    public List<Image> images = new List<Image>();

    public void SwitchActive(int id)
    {
        // Deactivate old image
        var activeColor = activeImage.color;
        activeColor.a = 0.25f;
        activeImage.color = activeColor;
        StartCoroutine(EasedLerpScale(activeImage.GetComponent<RectTransform>(), false));

        // Activate new image
        activeImage = images[id];
        activeColor = activeImage.color;
        activeColor.a = 1f;
        activeImage.color = activeColor;
        StartCoroutine(EasedLerpScale(activeImage.GetComponent<RectTransform>(), true));
    }
    
    public IEnumerator EasedLerpScale(RectTransform image, bool shouldEnlarge)
    {
        var t = 0f;
        var easedT = 0f;
        while (Mathf.Abs(t) < transitionTime)
        {
            easedT = Easing.Back.Out(t);
            var size = (shouldEnlarge ? minImageSize : maxImageSize) + easedT * (maxImageSize - minImageSize) * (shouldEnlarge ? 1 : -1);
            image.sizeDelta = Vector2.one * size;
            yield return new WaitForEndOfFrame(); // Surely this is poor practise.
            t += Time.deltaTime; // Is this the correct deltaTime for a IEnumerator?
        }
        yield break;
    }
}

