using UnityEngine;

namespace Items.Weapons
{
    public abstract class WeaponSettings : ItemSettings
    {
        [SerializeField] private int _damage;
        public int Damage => _damage;

        [SerializeField] private int _maxAmmunition;
        public int MaxAmmunition => _maxAmmunition;

        [SerializeField] private float _range;
        public float Range => _range;

        public abstract void Attack();
    }
}