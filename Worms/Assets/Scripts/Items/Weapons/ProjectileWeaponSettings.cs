using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileWeaponSettings", menuName = "ScriptableObjects/Weapon/ProjectileWeapon")]
public class ProjectileWeaponSettings : WeaponSettings
{
    public GameObject projectile;
    
    public float projectileSpeed;

    public override void Attack() // Hmm... since this is using projectiles, I feel like this should be in the monobehaviour...
    {
        Debug.Log("projectile pew");

        prefab.transform.parent = null;

        prefab.GetComponent<Rigidbody>().isKinematic = false;

        //prefab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        // Apply force in direction of camera
        var force = Camera.main.transform.forward * projectileSpeed;
        prefab.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

    }
    
}
