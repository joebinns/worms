using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRack : MonoBehaviour
{
    public List<GameObject> items;

    private int _currentIndex = 0;

    public SpawnEffect spawnEffect;

    public GameObject currentItem;

    
    private void Awake()
    {
        //currentItem = items[0];

        /*
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i].GetComponent(typeof(Item));
            Debug.Log(item.GetType());
            //item.id = i;
        }
        */
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

    public void ChangeItem(int index)
    {
        // Disable current game object
        items[_currentIndex].SetActive(false);

        // Enable newly indexed game object
        currentItem = items[index];
        currentItem.SetActive(true);

        // Reset transform
        currentItem.transform.localPosition = Vector3.zero;

        // Spawn effect
        spawnEffect.ActivateEffects(this.transform, currentItem);

        _currentIndex = index;
    }

    public void NextItem()
    {
        var index = (_currentIndex + 1) % items.Count;

        ChangeItem(index);

    }

    public void PreviousItem()
    {
        var index = (_currentIndex - 1);
        if (index < 0)
        {
            index += items.Count;
        }

        ChangeItem(index);

    }
}
