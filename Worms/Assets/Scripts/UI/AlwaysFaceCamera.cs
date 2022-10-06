using UnityEngine;

namespace UI
{
    public class AlwaysFaceCamera : MonoBehaviour
    {
        void Update() // Hmm... this seems slightly jittery (at short distances), regardless of whether it's in
                      // Update(), FixedUpdate() or LateUpdate().
        {
            this.transform.rotation = UnityEngine.Camera.main.transform.rotation;
        }
    }
}
