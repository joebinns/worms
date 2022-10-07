using Audio;
using Cameras;
using UnityEngine;
using Utilities;

namespace Items.Weapons
{
    public class ProjectileWeapon : Weapon
    {
        [SerializeField] private GameObject _renderer;
        [SerializeField] private GameObject _projectile;
        private ProjectileWeaponSettings ProjectileWeaponSettings => WeaponSettings as ProjectileWeaponSettings;

        private void OnEnable()
        {
            ShowWeapon();
            ResetProjectile();
        }
    
        // Make the weapon's renderer visible
        private void ShowWeapon()
        {
            _renderer.SetActive(true);
        }
    
        // Make the weapon's renderer invisible
        private void HideWeapon()
        {   
            _renderer.SetActive(false);
        }

        private void ResetProjectile()
        {
            // Reset the projectile
            _projectile.SetActive(false);
            _projectile.transform.parent = this.transform;
            StartCoroutine(UnityUtils.ResetRigidbody(_projectile.GetComponent<Rigidbody>(), Vector3.zero, Quaternion.identity));
        
            // Check the projectile's damage
            _projectile.GetComponent<Projectile>().Damage = ProjectileWeaponSettings.Damage;
        }

        public override void Attack()
        {
            if (CurrentAmmunition <= 0)
            {
                CinemachineShake.Instance.InvalidInputPresetShake();
                AudioManager.Instance.Play("Error");
                return;
            }

            var force = CalculateProjectileForce();
            ShootProjectile(force);

            DepleteAmmunition();
        }

        protected override void DepleteAmmunition()
        {
            base.DepleteAmmunition();

            if (CurrentAmmunition <= 0)
            {
                HideWeapon();
            }
        }

        private Vector3 CalculateProjectileForce()
        {
            var forceDirection = ApproximateShotCorrectionToCrosshair();
            var force = forceDirection * ProjectileWeaponSettings.ProjectileSpeed;
            return force;
        }
    
        /// <summary>
        /// Attempts to correct the projectile's shoot direction towards the crosshair.
        /// </summary>
        /// <returns></returns>
        private Vector3 ApproximateShotCorrectionToCrosshair()
        {
            var cameraTransform = UnityEngine.Camera.main.transform;
            var approxShotRange = ProjectileWeaponSettings.Range;

            Vector3 crosshairHitPosition;
            
            RaycastHit hit;
            var doesCrosshairHit = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, approxShotRange);
            if (doesCrosshairHit)
            {
                crosshairHitPosition = hit.point;
            }
            else
            {
                crosshairHitPosition = cameraTransform.position + cameraTransform.forward * approxShotRange;
            }
        
            var correctedDirection = (crosshairHitPosition - _projectile.transform.position).normalized;

            return correctedDirection;
        }

        private void ShootProjectile(Vector3 force)
        {
            // Activate projectile
            _projectile.SetActive(true);
            _projectile.transform.parent = null;

            // Apply force to projectile in direction of camera
            _projectile.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }

        #region Trajectory Guide (No longer used)
        /*
    [Header("Trajectory Guide")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _maxPhysicsFrameIterations = 10;
     
    private void Update()
    {
        if (currentAmmunition > 0)
        {
            var forceDirection = ApproximateShotCorrectionToCrosshair();

            _force = forceDirection * projectileWeaponSettings.projectileSpeed;
            
            DrawProjection();
        }
    } 
    
    private void DrawProjection()
    {
        _lineRenderer.enabled = true;
        
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
    */
        #endregion
    
    }
}

