using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public new string name;
    public int damage;
    public GameObject prefab;

    public virtual void Equip(Player player)
    {
        // Instantiate weapon prefab in player's weapon slot
    }

    public abstract void Attack();

}