using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [HideInInspector] public WeaponSettings weaponSettings;

    public int currentAmmunition; // THIS IS GOOD! SINCE EACH PLAYER SHOULD HAVE SEPARATE AMMO

    private void Awake()
    {
        if (itemSettings is WeaponSettings)
        {
            weaponSettings = itemSettings as WeaponSettings;
            ResetAmmunition();
        }

    }

    // Have a method for attacking?
    public void Attack()
    {
        if (currentAmmunition <= 0)
        {
            return;
        }

        // if so, then attack
        weaponSettings.Attack();

        // deplete ammunition
        currentAmmunition -= 1;

        // Update UI... Hmmm... UI manager should update remaining ammunition and max ammunition when changing weapons.
        // UI manager shoudl update remaining ammunition whenever attack is called.
    }

    public void ResetAmmunition() // Call this at the start of each turn... also we should increase player ride height
    {
        currentAmmunition = weaponSettings.maxAmmunition;
    }

}
