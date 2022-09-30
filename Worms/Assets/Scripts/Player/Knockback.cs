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
}
