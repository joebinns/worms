using UnityEngine;

namespace UI
{
    public class AlwaysFaceCamera : MonoBehaviour
    {
        void Update() // Hmm... this seems slightly jittery, regardless of whether it's Update(), FixedUpdate() or LateUpdate().
        {
            this.transform.rotation = UnityEngine.Camera.main.transform.rotation;
            //transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }
}
