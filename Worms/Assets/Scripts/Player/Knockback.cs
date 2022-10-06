using System;
using Items.Weapons;
using UnityEngine;

namespace Player
{
    public class Knockback : MonoBehaviour
    {
        private int _knockback = 0;
        private Rigidbody _rigidbody;
        public event Action<int> OnKnockbackChanged;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void ApplyKnockback(int damage, Vector3 direction, Vector3 position)
        {
            var knockbackMultiplier = 20f + (float)ChangeKnockback(damage);
            var force = CalculateForce(knockbackMultiplier, direction.normalized);
            _rigidbody.AddForceAtPosition(force, position, ForceMode.Impulse);
        }

        private int ChangeKnockback(int delta)
        {
            _knockback += delta;
            OnKnockbackChanged?.Invoke(_knockback);
            return _knockback;
        }

        private Vector3 CalculateForce(float magnitude, Vector3 direction)
        {
            var force = direction * magnitude;
            force.y = Mathf.Abs(force.y);

            return force;
        }
    }
}
