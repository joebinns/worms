using Items.Weapons;
using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected ItemSettings _itemSettings;
        public int id => _itemSettings.ID;
    }
}