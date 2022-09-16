using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static GameObject reticle;

    private void Awake()
    {
        reticle = FindObjectOfType<Reticle>().gameObject;
        DisableReticle();
    }

    public static void EnableReticle()
    {
        reticle.SetActive(true);
    }
    
    public static void DisableReticle()
    {
        reticle.SetActive(false);
    }
}
