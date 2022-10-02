using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    [HideInInspector] public WeaponSettings weaponSettings;

    [HideInInspector] public int currentAmmunition;

    private void Awake()
    {
        ResetAmmunition();
    }

    public abstract void Attack();

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
