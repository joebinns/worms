using UnityEditor.Rendering;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// The properties to store for a generic sound.
    /// </summary>
    [System.Serializable]
    public class Sound // Not sure how I feel about this implementation in comparison to a scriptable object...
    {
        #region Impermutable settings
        [SerializeField] private string _name;
        public string Name => _name;
        [SerializeField] private AudioClip _clip;
        public AudioClip Clip => _clip;
        [Range(0f, 1f)] [SerializeField] private float _volume;
        public float Volume => _volume;
        [Range(.1f, 3f)] [SerializeField] private float _pitch;
        public float Pitch => _pitch;
        [SerializeField] private bool _loop;
        public bool Loop => _loop;
        #endregion
        
        [HideInInspector]
        public AudioSource Source;
    }
}
