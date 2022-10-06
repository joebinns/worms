using UnityEngine;

namespace Items
{
    public class ItemSettings : ScriptableObject
    {
        [SerializeField] private int _id;
        public int ID => _id;

        [SerializeField] private string _name;
        public string Name => _name;

        [SerializeField] private GameObject _prefab;
        public GameObject Prefab => _prefab;
    }
}
