using UnityEngine;

namespace UI
{
    public class Reticle : MonoBehaviour
    {
        private RectTransform _reticle;
        private float _size;
        private bool _isZoomed;

        private void Awake()
        {
            _reticle = GetComponent<RectTransform>();
        }

        private void ResizeReticle(float reticleSize)
        {
            _size = reticleSize;
            _reticle.sizeDelta = new Vector2(_size, _size);
        }
    
        private void ZoomIn()
        {
            _isZoomed = true;
            ResizeReticle(30f);
        }

        private void ZoomOut()
        {
            _isZoomed = false;
            ResizeReticle(50f);
        }

        public void ToggleZoom()
        {
            if (_isZoomed)
            {
                ZoomOut();
            }
            else
            {
                ZoomIn();
            }
        }
    }
}
