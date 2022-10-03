using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private int _knockback = 0;

    // --> Subscribed to by material flasher, Healthplate.
    public event Action<int> OnKnockbackChanged; // Can I make this non static, so that not all nameplates are called?

    public int ChangeKnockback(int delta)
    {
        _knockback += delta;

        OnKnockbackChanged?.Invoke(_knockback);

        return _knockback;
    }
    
    // On enter trigger, if trigger is a projectile, change _knockback
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            var damage = collision.gameObject.GetComponent<Projectile>().damage;

            var knockback = ChangeKnockback(damage);

            var knockbackMultiplier = 10f + (float)knockback;

            // Apply force in direction of collision, which accounts for the players knockback
            var force = collision.GetContact(0).normal.normalized * knockbackMultiplier; // if normal doesn't work well, use impulse.normalized or relativeVelocity.normalized.
            force = force;
            force.y *= 1f;
            force.y = Mathf.Abs(force.y);

            gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, collision.GetContact(0).point, ForceMode.Impulse);
            
        }
        
    }
}
