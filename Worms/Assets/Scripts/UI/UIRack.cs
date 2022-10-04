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

    public Color deactiveColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public Color activeColor = Color.white;

    public float transitionTime = 1f;

    public List<Image> images = new List<Image>();

    public void SwitchActive(int id)
    {
        // Deactivate old image
        activeImage.color = deactiveColor;
        StartCoroutine(EasedLerpScale(activeImage.GetComponent<RectTransform>(), false));

        ActivateImage(id);
    }

    public void ActivateImage(int id)
    {
        // Activate new image
        activeImage = images[id];
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

