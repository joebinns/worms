using System.Collections.Generic;
using Audio;
using Unity.VisualScripting;
using UnityEngine;

namespace Items
{
    public class ItemRack : MonoBehaviour
    {
        public List<GameObject> Items;
        public GameObject CurrentItem;

        private int _currentIndex = 0;
        [SerializeField] private SpawnEffect _spawnEffect;

        public void ChangeItem(int index)
        {
            if (index == _currentIndex)
            {
                return;
            }

            // Disable previous game object
            Items[_currentIndex].SetActive(false);

            // Enable newly indexed game object
            CurrentItem = Items[index];
            CurrentItem.SetActive(true);

            // Reset transform
            CurrentItem.transform.localPosition = Vector3.zero;

            // Spawn effect
            _spawnEffect.ActivateEffects(this.transform, CurrentItem);

            _currentIndex = index;
        }

        public void NextItem()
        {
            AudioManager.Instance.Play("Click Primary");

            var index = (_currentIndex + 1) % Items.Count;

            ChangeItem(index);

        }

        public void PreviousItem()
        {
            AudioManager.Instance.Play("Click Primary");

            var index = (_currentIndex - 1);
            if (index < 0)
            {
                index += Items.Count;
            }

            ChangeItem(index);

        }
    }
}
