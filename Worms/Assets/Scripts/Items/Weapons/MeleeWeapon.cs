using Camera;
using UnityEngine;

namespace Items.Weapons
{
    public class MeleeWeapon : Weapon
    {
        public MeleeWeaponSettings MeleeWeaponSettings => WeaponSettings as MeleeWeaponSettings;
        [SerializeField] private Animator _animator;

        public override void Attack()
        {
            if (CurrentAmmunition <= 0)
            {
                CinemachineShake.Instance.InvalidInputPresetShake();
                return;
            }

            WeaponSettings.Attack();

            // Play animation
            _animator.SetTrigger("Attack");

            // Deplete ammunition
            DepleteAmmunition();
        }
    }
}
