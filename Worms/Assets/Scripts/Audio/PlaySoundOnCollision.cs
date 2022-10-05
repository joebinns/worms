using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Play a given sound when this rigid body / collider collides.
    /// </summary>
    public class PlaySoundOnCollision : MonoBehaviour
    {
        [SerializeField] private string _audioName;

        /// <summary>
        /// Play the sound when this rigid body / collider collides with another.
        /// </summary>
        /// <param name="other">The other rigid body / collider involved in this collision.</param>
        private void OnCollisionEnter(Collision other)
        {
            AudioManager.Instance.Play(_audioName);
        }
    }
}
