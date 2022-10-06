using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Easing : MonoBehaviour
    {
        #region Not working as intended
        public static IEnumerator<float> EasedLerp(float start, float end, float duration, bool isForward = true)
        {
            var t = 0f;
            var easedT = 0f;
            while (Mathf.Abs(t) < duration)
            {
                easedT = EasingUtils.Back.Out(t);
                var size = (isForward ? start : end) + easedT * (end - start) * (isForward ? 1 : -1);
                t += Time.deltaTime;
                yield return size;
            }
        }
        #endregion
    }
}
