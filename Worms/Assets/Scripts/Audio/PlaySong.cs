using UnityEngine;

namespace Audio
{
    public class PlaySong : MonoBehaviour
    {
        [SerializeField] private string _audioName;

        private void Start()
        {
            AudioManager.Instance.Play(_audioName);
        }
    }
}
