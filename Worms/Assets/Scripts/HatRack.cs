using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatRack : MonoBehaviour
{
    public List<GameObject> hats;

    private int _currentIndex = 0;

    public SpawnEffect spawnEffect;

    public GameObject currentHat;

    private void Awake()
    {
        //_previousIndex = hats.Count;
        for (int i = 0; i < hats.Count; i++)
        {
            hats[i].GetComponent<Hat>().id = i;
        }
    }

    /*
    public int FindHatIndex(GameObject hat)
    {
        var index = hats.IndexOf(hat);
        return index;
    }


    public void ChangeHat(GameObject hat)
    {
        Debug.Log(hat.name);
        var index = FindHatIndex(hat);
        ChangeHat(index);
    }
    */

    public void ChangeHat(int index)
    {
        // Disable current game object
        hats[_currentIndex].SetActive(false);

        // Enable newly indexed game object
        currentHat = hats[index];
        currentHat.SetActive(true);

        // Spawn effect
        spawnEffect.ActivateEffects(this.transform, currentHat);

        _currentIndex = index;
    }

    public void NextHat()
    {
        var index = (_currentIndex + 1) % hats.Count;

        ChangeHat(index);

    }

    public void PreviousHat()
    {
        var index = (_currentIndex - 1);
        if (index < 0)
        {
            index += hats.Count;
        }

        ChangeHat(index);

    }
}
