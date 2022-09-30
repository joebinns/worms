using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [HideInInspector] public WeaponSettings weaponSettings;
    
    private void Awake()
    {
        /*
        if (!(itemSettings is WeaponSettings))
        {
            return;
        }
        */
        if (itemSettings is WeaponSettings)
        {
            weaponSettings = itemSettings as WeaponSettings;
        }

        //id = (itemSettings as WeaponSettings).id;
    }
}
