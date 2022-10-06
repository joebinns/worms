using Players;
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
            
            // Deal damage to the player hit, and determine the appropriate knockback force to apply
            var knockback = raycastHit.collider.GetComponent<Knockback>();
            knockback.ApplyKnockback(Damage, cameraTransform.forward, raycastHit.point);
        }
    }
}
