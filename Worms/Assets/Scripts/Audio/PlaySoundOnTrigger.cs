using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Play a given sound when this rigid body / collider enters a trigger.
    /// </summary>
    public class PlaySoundOnTrigger : MonoBehaviour
    {
        [SerializeField] private string _audioName;

        /// <summary>
        /// Play the sound when this rigid body / collider enters a trigger.
        /// </summary>
        /// <param name="other">The trigger's rigid body / collider.</param>
        private void OnTriggerEnter(Collider other)
        {
            AudioManager.Instance.Play(_audioName);
        }
    }
}
