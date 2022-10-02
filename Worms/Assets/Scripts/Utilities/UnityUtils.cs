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

    public static IEnumerator ResetRigidbody(Rigidbody rb, Vector3 targetLocalPos, Quaternion targetLocalRot)
    {
        // Reset velocity and angular velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
 
        yield return new WaitForFixedUpdate();
 
        // Reset position and rotation
        rb.transform.localPosition = targetLocalPos;
        rb.transform.localRotation = targetLocalRot;
    }
}
