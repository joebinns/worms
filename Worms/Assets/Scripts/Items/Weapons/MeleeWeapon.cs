using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void Attack()
    {
        if (currentAmmunition <= 0)
        {
            return;
        }

        weaponSettings.Attack();

        // Deplete ammunition
        currentAmmunition--;
    }
}
