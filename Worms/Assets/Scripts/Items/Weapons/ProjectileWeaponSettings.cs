using Unity.VisualScripting;
using UnityEngine;

namespace Items.Weapons
{
    [CreateAssetMenu(fileName = "ProjectileWeaponSettings", menuName = "ScriptableObjects/Weapon/ProjectileWeapon")]
    public class ProjectileWeaponSettings : WeaponSettings
    {
        public float ProjectileSpeed;

        public override void Attack() // The projectile attack behaviour is instead occuring in ProjectileWeapons.cs, due to use of scene instanced references
        {
        }
    }
}
