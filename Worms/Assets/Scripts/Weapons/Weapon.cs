using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public new string name;
    public int damage;
    public GameObject prefab;

    public abstract void Attack();

}