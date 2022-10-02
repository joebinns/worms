using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    private ProjectileWeaponSettings projectileWeaponSettings
    {
        get
        {
            return weaponSettings as ProjectileWeaponSettings;
        }
    }

    private void OnEnable()
    {
        // Make this game object visible
        renderer.SetActive(true);

        // Reset the projectile
        projectile.SetActive(false);
        projectile.transform.parent = this.transform;
        StartCoroutine(UnityUtils.ResetRigidbody(projectile.GetComponent<Rigidbody>(), Vector3.zero, Quaternion.identity));
    }

    [SerializeField] private new GameObject renderer;
    [SerializeField] private GameObject projectile;

    public override void Attack()
    {
        if (currentAmmunition <= 0)
        {
            return;
        }

        weaponSettings.Attack();




        // Activate projectile --> projectile should be a child in the prefab
        projectile.SetActive(true);
        projectile.transform.parent = null;

        // Apply force to projectile in direction of camera
        var force = Camera.main.transform.forward * projectileWeaponSettings.projectileSpeed;
        projectile.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);




        // Deplete ammunition
        currentAmmunition--;

        if (currentAmmunition <= 0)
        {
            // Make this game object invisible
            renderer.SetActive(false);
        }

    }

    public override void ResetAmmunition()
    {
        base.ResetAmmunition();

    }
}

