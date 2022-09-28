using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponSettings", menuName = "ScriptableObjects/Weapon/MeleeWeapon")]
public class MeleeWeapon : Weapon
{
    public override void Attack()
    {
        Debug.Log("melee bonk");
        
        // Raycast ahead a short distance
        //PlayerManager.currentPlayer
        
        // If raycast collides, deal damage to Health.cs
    }
}
