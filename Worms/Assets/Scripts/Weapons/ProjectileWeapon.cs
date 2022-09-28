using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileWeaponSettings", menuName = "ScriptableObjects/Weapon/ProjectileWeapon")]
public class ProjectileWeapon : Weapon
{
    public float projectileSpeed;

    public override void Attack()
    {
        Debug.Log("projectile pew");
    }
}
