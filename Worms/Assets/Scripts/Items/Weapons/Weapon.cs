using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [HideInInspector] public WeaponSettings weaponSettings;

    [HideInInspector] public int currentAmmunition;

    private void Awake()
    {
        ResetAmmunition();
    }

    public void Attack()
    {
        if (currentAmmunition <= 0)
        {
            return;
        }

        weaponSettings.Attack();

        // Deplete ammunition
        currentAmmunition--;
    }

    // Call this at the start of each turn
    public void ResetAmmunition()
    {
        if (itemSettings is WeaponSettings) // This needs to be here rather than Awake, in case ResetAmmunition is called before Awake... scriptable object behaviour stuff
        {
            weaponSettings = itemSettings as WeaponSettings;
            currentAmmunition = weaponSettings.maxAmmunition;
        }
    }

}
