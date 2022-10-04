using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private MeleeWeaponSettings meleeWeaponSettings
    {
        get
        {
            return weaponSettings as MeleeWeaponSettings;
        }
    }

    [SerializeField] private Animator _animator;

    public override void Attack()
    {
        if (currentAmmunition <= 0)
        {
            CinemachineShake.Instance.ShakeCamera(2.5f, 0.4f, "1D Wobble");
            return;
        }

        weaponSettings.Attack();

        // Play animation
        _animator.SetTrigger("Attack");

        // Deplete ammunition
        currentAmmunition--;
    }
}
