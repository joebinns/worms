using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponSettings", menuName = "ScriptableObjects/Weapon/MeleeWeapon")]
public class MeleeWeapon : Weapon
{
    public float range = 7f; // Note that this is distance from the camera, and not the player... Should be adjusted in the future.

    public override void Attack()
    {
        Debug.Log("melee bonk");

        // Play animation

        // Raycast from the camera a short distance
        RaycastHit raycastHit;
        var layermask = LayerMask.GetMask("Player Orange", "Player Cyan", "Player Pink", "Player Lime");
        bool didRaycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, range, layermask);
        
        // If raycast collides with another Player, deal damage to other Player's Health.cs
        if (didRaycastHit)
        {
            Debug.Log(raycastHit.collider.name);

            //raycastHit.collider.GetComponent<Health>().health -= damage;
            raycastHit.collider.GetComponent<Health>().ChangeHealth(-damage);
        }

    }
}
