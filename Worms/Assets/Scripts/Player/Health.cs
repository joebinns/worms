using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _health = 100;
    private bool _isDead = false;
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


    public void ChangeHealth(int delta)
    {
        if (_isDead)
        {
            return;
        }

        _health += delta;

        // Flash materials white

        if (_health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        _health = 0;

        GetComponent<Player>().UpdatePlayerState(PlayerState.Dead);

        _isDead = true;
    }
}
