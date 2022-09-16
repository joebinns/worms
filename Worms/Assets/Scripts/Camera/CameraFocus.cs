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

    public void SetFocus(GameObject focusGameObject)
    {
        playerPositionFollow.player = focusGameObject;
        aimCamera.GetComponent<CinemachineVirtualCamera>().Follow = focusGameObject.transform;
    }

}
