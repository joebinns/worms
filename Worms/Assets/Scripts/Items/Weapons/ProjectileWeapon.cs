using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private new GameObject renderer;
    [SerializeField] private GameObject projectile;
    private ProjectileWeaponSettings projectileWeaponSettings
    {
        get
        {
            return weaponSettings as ProjectileWeaponSettings;
        }
    }

    [Header("Display Controls")]
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    [Range(10, 100)]
    private int _linePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float _timeBetweenPoints = 0.1f;

    private void OnEnable()
    {
        // Make this game object visible
        renderer.SetActive(true);

        // Reset the projectile
        projectile.SetActive(false);
        projectile.transform.parent = this.transform;
        StartCoroutine(UnityUtils.ResetRigidbody(projectile.GetComponent<Rigidbody>(), Vector3.zero, Quaternion.identity));
        
        // Set the projectile's damage
        projectile.GetComponent<Projectile>().damage = projectileWeaponSettings.damage;

    }

    private Vector3 _force;    
    
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
        projectile.GetComponent<Rigidbody>().AddForce(_force, ForceMode.Impulse);




        // Deplete ammunition
        currentAmmunition--;

        if (currentAmmunition <= 0)
        {
            // Make this game object invisible
            renderer.SetActive(false);
        }

    }

    private void Update()
    {
        if (currentAmmunition > 0)
        {
            _force = Camera.main.transform.forward * projectileWeaponSettings.projectileSpeed;
            DrawProjection();
        }
    }

    private void DrawProjection()
    {
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = Mathf.CeilToInt(_linePoints / _timeBetweenPoints) + 1;
        Vector3 startPosition = transform.position;
        Vector3 startVelocity = _force / projectile.GetComponent<Rigidbody>().mass;
        int i = 0;
        _lineRenderer.SetPosition(i, startPosition);
        for (float t = 0; t < _linePoints; t += _timeBetweenPoints)
        {
            i++;
            
            Vector3 point = startPosition + t * startVelocity;
            point.y = startPosition.y + startVelocity.y * t + (0.5f * Physics.gravity.y * Mathf.Pow(t, 2)); // Kinematic equation
            
            _lineRenderer.SetPosition(i, point);
        }
    }
    
}

