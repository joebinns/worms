using UnityEngine;

namespace Utilities
{
    // This script is used as a copy of a transform which doesn't account for rotation or scale.
    // Intended for use as a fixed-angle target transform for Cinemachine.
    public class FollowPosition : MonoBehaviour
    {
        public GameObject Target;
    
        public void FixedUpdate()
        {
            this.transform.position = Target.transform.position;
        }
    }
}
