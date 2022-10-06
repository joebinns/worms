using UnityEngine;

namespace Items.Hats
{
    public class Hat : Item
    {
        public HatSettings HatSettings => _itemSettings as HatSettings;
    }
}
