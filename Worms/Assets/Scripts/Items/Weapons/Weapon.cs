using UnityEngine;

namespace Items.Weapons
{
    public abstract class Weapon : Item
    {
        [HideInInspector] public WeaponSettings WeaponSettings
        {
            get
            {
                return itemSettings as WeaponSettings;
            }
        }

        [HideInInspector] public int CurrentAmmunition;

        private void Awake()
        {
            ResetAmmunition();
        }

        public abstract void Attack();

        // Call this at the start of each turn
        public virtual void ResetAmmunition()
        {
            CurrentAmmunition = WeaponSettings.maxAmmunition;
        }

    }
}
