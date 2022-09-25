using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamePlate : MonoBehaviour
{
    void Start()
    {
        this.transform.rotation = Camera.main.transform.rotation; // Causes the text faces camera.
    }
}
