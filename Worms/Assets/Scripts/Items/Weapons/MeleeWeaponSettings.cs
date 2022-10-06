using UnityEngine;

namespace Items.Weapons
{
    [CreateAssetMenu(fileName = "MeleeWeaponSettings", menuName = "ScriptableObjects/Weapon/MeleeWeapon")]
    public class MeleeWeaponSettings : WeaponSettings
    {
        public override void Attack()
        {
            // Raycast from the camera a short distance
            var layermask = LayerMask.GetMask("Player Orange", "Player Cyan", "Player Pink", "Player Lime");
            var cameraTransform = UnityEngine.Camera.main.transform;
            // Note that Range is distance from the camera, and not the player
            var didRaycastHit = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var raycastHit, Range, layermask);
            
            if (!didRaycastHit) return;
            
            // If raycast collides with another Player, deal damage to other Player's Health.cs
            var knockback = raycastHit.collider.GetComponent<Knockback>().ChangeKnockback(Damage);
            var knockbackMultiplier = 10f + (float)knockback;

            // Apply force in same direction as raycast, which accounts for the players knockback
            var force = cameraTransform.forward * knockbackMultiplier;
            force.y *= 1f;
            force.y = Mathf.Abs(force.y);
            
            raycastHit.collider.GetComponent<Rigidbody>().AddForceAtPosition(force, raycastHit.point, ForceMode.Impulse);
        }
    }
}
