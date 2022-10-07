using UnityEngine;

namespace Cameras
{
    public class SkyboxRotator : MonoBehaviour
    {
        private const float ROTATIONAL_SPEED = -1f;
        
        void Update ()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * ROTATIONAL_SPEED);
        }
    }
}
