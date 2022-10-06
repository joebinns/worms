using UnityEngine;

namespace Items.Weapons
{
    public abstract class Weapon : Item
    {
        public WeaponSettings WeaponSettings => _itemSettings as WeaponSettings;
        [HideInInspector] public int CurrentAmmunition;

        private void Awake()
        {
            ResetAmmunition();
        }

        public abstract void Attack();

        // Call this at the start of each turn
        public void ResetAmmunition()
        {
            CurrentAmmunition = WeaponSettings.MaxAmmunition;
        }

        protected virtual void DepleteAmmunition()
        {
            CurrentAmmunition--;
        }

    }
}
