using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle;

    [Range(50f, 100f)]
    public float size = 100f;

    private void Awake()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        reticle.sizeDelta = new Vector2(size, size);
    }
}
