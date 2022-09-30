using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponSettings", menuName = "ScriptableObjects/Weapon/MeleeWeapon")]
public class MeleeWeaponSettings : WeaponSettings
{
    public float range = 7.5f; // Note that this is distance from the camera, and not the player... Should be adjusted in the future.

    public override void Attack()
    {
        // Play animation

        // Raycast from the camera a short distance
        RaycastHit raycastHit;
        var layermask = LayerMask.GetMask("Player Orange", "Player Cyan", "Player Pink", "Player Lime");
        bool didRaycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, range, layermask);
        
        // If raycast collides with another Player, deal damage to other Player's Health.cs
        if (didRaycastHit)
        {
            var knockback = raycastHit.collider.GetComponent<Knockback>().ChangeKnockback(damage);
            var knockbackMultiplier = 10f + (float)knockback;

            // Apply force in same direction as raycast, which accounts for the players knockback
            var force = Camera.main.transform.forward * knockbackMultiplier;
            force.y *= 1f;
            force.y = Mathf.Abs(force.y);
            
            raycastHit.collider.GetComponent<Rigidbody>().AddForceAtPosition(force, raycastHit.point, ForceMode.Impulse);

        }

    }
    
}
