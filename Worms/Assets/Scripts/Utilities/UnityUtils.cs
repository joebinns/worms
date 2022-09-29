using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityUtils
{
    public static void GetAllChildren(Transform parent, ref List<Transform> transforms)
    {
        foreach (Transform child in parent)
        {
            transforms.Add(child);
 
            GetAllChildren(child, ref transforms);
 
        }
 
    }
}
