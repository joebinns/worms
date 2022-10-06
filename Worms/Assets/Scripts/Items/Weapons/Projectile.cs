using Players;
using UnityEngine;
using Utilities;

namespace Items.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [HideInInspector] public int Damage;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != CommonLayerMasks.Players) return;
            
            var collisionContact = collision.GetContact(0);
            
            // Deal damage to the player hit, and determine the appropriate knockback force to apply
            var knockback = collision.collider.GetComponent<Knockback>();
            knockback.ApplyKnockback(Damage, collisionContact.normal, collisionContact.point);
        }
    }
}
