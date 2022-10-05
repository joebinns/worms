using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraFocus : MonoBehaviour
    {
        public FollowPosition PlayerFollowPosition;
    
        [Header("Optional")]
        public CinemachineVirtualCamera AimCamera;
    
        private void OnEnable()
        {
            PlayerManager.Instance.OnPlayerChanged += SetFocus;
        }

        private void OnDisable()
        {
            PlayerManager.Instance.OnPlayerChanged -= SetFocus;
        }

        private void Start()
        {
            SetFocus(PlayerManager.Instance.currentPlayer);
        }

        private void SetFocus(Player.Player focusPlayer)
        {
            PlayerFollowPosition.target = focusPlayer.gameObject;
            if (AimCamera != null)
            {
                AimCamera.Follow = focusPlayer.transform;
            }
        }
    }
}
