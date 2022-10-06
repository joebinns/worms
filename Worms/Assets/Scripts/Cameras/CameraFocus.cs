using Cinemachine;
using Players;
using UnityEngine;
using Utilities;

namespace Cameras
{
    public class CameraFocus : MonoBehaviour
    {
        [SerializeField] private FollowPosition _playerFollowPosition;

        #region Optional
        [Header("Optional")]
        [SerializeField] private CinemachineVirtualCamera _aimCamera;
        #endregion

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

        private void SetFocus(Player focusPlayer)
        {
            _playerFollowPosition.Target = focusPlayer.gameObject;
            if (_aimCamera != null)
            {
                _aimCamera.Follow = focusPlayer.transform;
            }
        }
    }
}
