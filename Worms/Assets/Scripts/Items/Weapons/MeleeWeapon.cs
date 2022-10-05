using System.Collections;
using System.Collections.Generic;
using Items.Weapons;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private MeleeWeaponSettings meleeWeaponSettings
    {
        get
        {
            return WeaponSettings as MeleeWeaponSettings;
        }
    }

    [SerializeField] private Animator _animator;

    public override void Attack()
    {
        if (CurrentAmmunition <= 0)
        {
            CinemachineShake.Instance.ShakeCamera(2.5f, 0.4f, "1D Wobble");
            return;
        }

        WeaponSettings.Attack();

        // Play animation
        _animator.SetTrigger("Attack");

        // Deplete ammunition
        CurrentAmmunition--;
    }
}
