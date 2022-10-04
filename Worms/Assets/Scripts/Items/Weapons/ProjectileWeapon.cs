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
    [SerializeField] private int _maxPhysicsFrameIterations = 10;

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
            CinemachineShake.Instance.ShakeCamera(2.5f, 0.4f, "1D Wobble");
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
            _lineRenderer.enabled = false;
        }

    }

    private void Update()
    {
        if (currentAmmunition > 0)
        {
            _force = Camera.main.transform.forward;
            //_force.y *= 4f;
            //_force = _force.normalized;
            _force *= projectileWeaponSettings.projectileSpeed;
            
            DrawProjection();
        }
    }

    private void DrawProjection()
    {
        _lineRenderer.enabled = true;
        //_lineRenderer.positionCount = Mathf.CeilToInt(_linePoints / _timeBetweenPoints) + 1;
        
        _lineRenderer.positionCount = _maxPhysicsFrameIterations;
        
        Vector3 startPosition = transform.position;
        Vector3 startVelocity = _force / projectile.GetComponent<Rigidbody>().mass;

        
        _lineRenderer.SetPosition(0, startPosition);
        
        for (var i = 1; i < _maxPhysicsFrameIterations; i++)
        {
            var time = i * Time.fixedDeltaTime;
            
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (0.5f * Physics.gravity.y * Mathf.Pow(time, 2)); // Kinematic equation
            
            _lineRenderer.SetPosition(i, point);
        }

    }
    
}

