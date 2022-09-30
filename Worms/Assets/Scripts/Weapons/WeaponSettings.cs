using UnityEngine;

public abstract class WeaponSettings : ItemSettings
{
    public int damage;

    public abstract void Attack();

}