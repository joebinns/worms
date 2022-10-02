using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRack : MonoBehaviour
{
    public List<GameObject> items;

    private int _currentIndex = 0;

    public SpawnEffect spawnEffect;

    public GameObject currentItem;

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
