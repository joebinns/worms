using UnityEngine;

namespace Items.Hats
{
    public class Hat : Item
    {
        public HatSettings HatSettings => _itemSettings as HatSettings; // hmm.. i need this to be serialized...
    }
}
