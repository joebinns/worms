using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    [HideInInspector] public WeaponSettings weaponSettings
    {
        get
        {
            return itemSettings as WeaponSettings;
        }
    }

    [HideInInspector] public int currentAmmunition;

    private void Awake()
    {
        ResetAmmunition();
    }

    public abstract void Attack();

    // Call this at the start of each turn
    public virtual void ResetAmmunition()
    {
        currentAmmunition = weaponSettings.maxAmmunition;
    }

}
