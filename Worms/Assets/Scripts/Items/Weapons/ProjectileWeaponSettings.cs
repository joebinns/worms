using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileWeaponSettings", menuName = "ScriptableObjects/Weapon/ProjectileWeapon")]
public class ProjectileWeaponSettings : WeaponSettings
{
    public float projectileSpeed;
    public float approxShotRange;

    public override void Attack() // Hmm... since this is using projectiles, I feel like this should be in the monobehaviour...
    {


    }
    
}
