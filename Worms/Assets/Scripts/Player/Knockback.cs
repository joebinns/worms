using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private int _knockback = 0;
    // private bool _isDead = false;
    /*
    public int health {
        get => _health;
        set {
            _health = value;
            if (_health <= 0)
            {
                Die();
            }
        }
    }
    */
    /*
    public int health {
        get => _health;
        set => value;
    }
    */
    //public int health;

    // --> Subscribed to by material flasher, Healthplate.
    public event Action<int> OnKnockbackChanged; // Can I make this non static, so that not all nameplates are called?

    public int ChangeKnockback(int delta)
    {
        /*
        if (_isDead)
        {
            return _knockback;
        }
        */

        _knockback += delta;
        
        Debug.Log(_knockback);

        OnKnockbackChanged?.Invoke(_knockback);

        return _knockback;
    }

    /*
    private void Die() // This is no longer a needed state, if using the percentage knockback system.
    {
        //_health = 0;

        GetComponent<Player>().UpdatePlayerState(PlayerState.Dead);

        _isDead = true;

    }
    */
}
