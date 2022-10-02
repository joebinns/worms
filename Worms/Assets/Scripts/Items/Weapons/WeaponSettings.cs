using UnityEngine;

public abstract class WeaponSettings : ItemSettings
{
    public int damage;
    public int maxAmmunition;

    public abstract void Attack();

}