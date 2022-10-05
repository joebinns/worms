using UnityEngine;

namespace Items.Weapons
{
    [CreateAssetMenu(fileName = "MeleeWeaponSettings", menuName = "ScriptableObjects/Weapon/MeleeWeapon")]
    public class MeleeWeaponSettings : WeaponSettings
    {
        public float Range = 7.5f; // Note that this is distance from the camera, and not the player... Should be adjusted in the future.

        public override void Attack()
        {
            // Raycast from the camera a short distance
            var layermask = LayerMask.GetMask("Player Orange", "Player Cyan", "Player Pink", "Player Lime");
            var didRaycastHit = Physics.Raycast(UnityEngine.Camera.main.transform.position, UnityEngine.Camera.main.transform.forward, out var raycastHit, Range, layermask);
        
            // If raycast collides with another Player, deal damage to other Player's Health.cs
            if (didRaycastHit)
            {
                var knockback = raycastHit.collider.GetComponent<Knockback>().ChangeKnockback(damage);
                var knockbackMultiplier = 10f + (float)knockback;

                // Apply force in same direction as raycast, which accounts for the players knockback
                var force = UnityEngine.Camera.main.transform.forward * knockbackMultiplier;
                force.y *= 1f;
                force.y = Mathf.Abs(force.y);
            
                raycastHit.collider.GetComponent<Rigidbody>().AddForceAtPosition(force, raycastHit.point, ForceMode.Impulse);

            }

        }
    
    }
}
