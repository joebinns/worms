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
            // Do nothing if the collision game object's layer indicates that it not a player
            if (CommonLayerMasks.Players != (CommonLayerMasks.Players | (1 << collision.gameObject.layer))) { return; }
            
            var collisionContact = collision.GetContact(0);
            
            // Deal damage to the player hit, and determine the appropriate knockback force to apply
            var knockback = collision.collider.GetComponent<Knockback>();
            knockback.ApplyKnockback(Damage, -collisionContact.normal, collisionContact.point);
        }
    }
}
