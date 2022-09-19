using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static Reticle reticle;

    private void Awake()
    {
        reticle = FindObjectOfType<Reticle>();
        DisableReticle();
    }

    public static void EnableReticle()
    {
        reticle.gameObject.SetActive(true);
    }
    
    public static void DisableReticle()
    {
        reticle.gameObject.SetActive(false);
    }

    public static void ToggleReticleZoom()
    {
        reticle.ToggleZoom();
    }
}
