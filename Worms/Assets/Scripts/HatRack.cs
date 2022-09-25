using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatRack : MonoBehaviour
{
    public List<GameObject> hats;

    private int _currentIndex = 0;

    private void Awake()
    {
        //_previousIndex = hats.Count;
    }

    private void ChangeHat(int index)
    {
        // Disable current game object
        hats[_currentIndex].SetActive(false);

        // Enable next indexed game object
        hats[index].SetActive(true);
    }

    public void NextHat()
    {
        var index = (_currentIndex + 1) % hats.Count;

        ChangeHat(index);

        _currentIndex = index;

    }

    public void PreviousHat()
    {
        var index = (_currentIndex - 1);
        if (index < 0)
        {
            index += hats.Count;
        }

        ChangeHat(index);

        _currentIndex = index;

    }
}
