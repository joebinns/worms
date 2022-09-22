using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public FollowPosition playerPositionFollow;
    public GameObject aimCamera;
    
    private void Awake()
    {
        PlayerManager.OnPlayerChanged += SetFocus;
    }

    private void Start()
    {
        SetFocus(PlayerManager.currentPlayer);
    }

    public void SetFocus(Player focusPlayer)
    {
        playerPositionFollow.player = focusPlayer.gameObject;

        if (aimCamera != null)
        {
            aimCamera.GetComponent<CinemachineVirtualCamera>().Follow = focusPlayer.transform;
        }

    }
}
