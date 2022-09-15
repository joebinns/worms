using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int _turn = 0;
    
    private void NextTurn()
    {
        _turn++;
        _turn %= PlayerManager.numPlayers;
    }
}
