using Unity.Burst.Intrinsics;
using UnityEngine;
using Utilities;

namespace Items.Weapons
{
    [CreateAssetMenu(fileName = "MeleeWeaponSettings", menuName = "ScriptableObjects/Weapon/MeleeWeapon")]
    public class MeleeWeaponSettings : WeaponSettings
    {
        public override void Attack()
        {
            var cameraTransform = UnityEngine.Camera.main.transform;
            var didRaycastHit = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var raycastHit, Range, CommonLayerMasks.Players); // Note that Range is distance from the camera, and NOT distance from the player
            
            if (!didRaycastHit) return;
            
            // Deal damage to the player hit, and determine the appropiate knockback force to apply
            var knockbackMultiplier = 10f + DealDamage(raycastHit.collider.GetComponent<Knockback>());
            var force = CalculateForce(knockbackMultiplier, cameraTransform.forward);
            
            // Apply knockback force
            raycastHit.collider.GetComponent<Rigidbody>().AddForceAtPosition(force, raycastHit.point, ForceMode.Impulse);
        }

        private float DealDamage(Knockback knockback)
        {
            // If raycast collides with another player, deal damage to other player's Health.cs
            return (float)knockback.ChangeKnockback(Damage);
        }

        private Vector3 CalculateForce(float magnitude, Vector3 direction)
        {
            // Apply force in same direction as raycast, whilst accounting for the player's knockback
            var force = direction * magnitude;
            force.y = Mathf.Abs(force.y);

            return force;
        }
    }
}
