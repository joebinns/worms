using UnityEngine;

public abstract class Weapon : Item
{
    public int damage;

    public abstract void Attack();

}