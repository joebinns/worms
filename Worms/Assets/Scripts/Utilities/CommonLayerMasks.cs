using UnityEngine;

namespace Utilities
{
    public static class CommonLayerMasks
    {
        public static int Players => LayerMask.GetMask("Player Orange", "Player Cyan", "Player Pink", "Player Lime");
    }
}
