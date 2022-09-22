using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FollowPosition : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        player = PlayerManager.currentPlayer.gameObject;
    }

    public void FixedUpdate()
    {
        this.transform.position = player.transform.position;
    }
}
