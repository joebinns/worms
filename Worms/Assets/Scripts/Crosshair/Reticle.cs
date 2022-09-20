using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle;

    private float size;

    private bool _isZoomed;

    private void Awake()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void ResizeReticle(float reticleSize)
    {
        size = reticleSize;
        reticle.sizeDelta = new Vector2(size, size);
    }
    
    private void ZoomIn()
    {
        _isZoomed = true;
        ResizeReticle(50f);
    }

    private void ZoomOut()
    {
        _isZoomed = false;
        ResizeReticle(100f);
    }

    public void ToggleZoom()
    {
        if (_isZoomed)
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }
}
