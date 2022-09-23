using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used as a copy of a transform which doesn't account for rotation or scale.
// Intended for use aa a fixed-angle target transform for Cinemachine.
public class FollowPosition : MonoBehaviour
{
    public GameObject target;
    
    public void FixedUpdate()
    {
        this.transform.position = target.transform.position;
    }
}
