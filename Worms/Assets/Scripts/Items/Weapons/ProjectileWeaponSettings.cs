using Unity.VisualScripting;
using UnityEngine;

namespace Items.Weapons
{
    [CreateAssetMenu(fileName = "ProjectileWeaponSettings", menuName = "ScriptableObjects/Weapon/ProjectileWeapon")]
    public class ProjectileWeaponSettings : WeaponSettings
    {
        #region Impermutable
        [SerializeField] private float _projectileSpeed; // 52f
        public float ProjectileSpeed => _projectileSpeed;
        #endregion

        // Since scene instanced references are used for projectile attacks, I didn't have much use for this,
        // but it makes things a little confusing (having an attack method in both scriptable objects and mono
        // behaviours)
        public override void Attack()
        {
        }
    }
}
