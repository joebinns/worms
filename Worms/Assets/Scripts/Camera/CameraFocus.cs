using Cinemachine;
using Player;
using UnityEngine;
using Utilities;

namespace Camera
{
    public class CameraFocus : MonoBehaviour
    {
        public FollowPosition PlayerFollowPosition;
    
        [Header("Optional")]
        public CinemachineVirtualCamera AimCamera;
    
        private void OnEnable()
        {
            PlayerManager.Instance.OnCurrentPlayerChanged += SetFocus;
        }

        private void OnDisable()
        {
            PlayerManager.Instance.OnCurrentPlayerChanged -= SetFocus;
        }

        private void Start()
        {
            SetFocus(PlayerManager.Instance.CurrentPlayer);
        }

        private void SetFocus(Player.Player focusPlayer)
        {
            PlayerFollowPosition.Target = focusPlayer.gameObject;
            if (AimCamera != null)
            {
                AimCamera.Follow = focusPlayer.transform;
            }
        }
    }
}
