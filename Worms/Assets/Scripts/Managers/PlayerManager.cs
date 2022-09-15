using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static List<Player> _players = new List<Player>();
    public static int numPlayers => _players.Count;

    private void Start()
    {
        _players.Add(FindObjectOfType<Player>().GetComponent<Player>()); // Temporarily doing this, prior to player selection implementation
    }

}

