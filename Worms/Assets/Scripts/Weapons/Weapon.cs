using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public string name;
    public float damage;
    public GameObject prefab;

    public abstract void Attack();

}