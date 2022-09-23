using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public FollowPosition playerFollowPosition;
    
    [Header("Optional")]
    public GameObject aimCamera;
    
    private void OnEnable()
    {
        PlayerManager.OnPlayerChanged += SetFocus;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerChanged -= SetFocus;
    }

    private void Start()
    {
        SetFocus(PlayerManager.currentPlayer);
    }

    public void SetFocus(Player focusPlayer)
    {
        playerFollowPosition.target = focusPlayer.gameObject;
        if (aimCamera != null)
        {
            aimCamera.GetComponent<CinemachineVirtualCamera>().Follow = focusPlayer.transform;
        }
    }
}
