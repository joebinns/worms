using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RawImageScroller : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;
        [SerializeField] private Vector2 _velocity;

        private void Update()
        {
            _rawImage.uvRect = new Rect(_rawImage.uvRect.position + _velocity * Time.deltaTime, _rawImage.uvRect.size);
        }
    }
}
