using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class UIRack : MonoBehaviour
    {
        [SerializeField] private float _minImageSize = 35f;
        [SerializeField] private float _maxImageSize = 40f;
        [SerializeField] private Color _deactiveColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        [SerializeField] Color _activeColor = Color.white;
        [SerializeField] protected List<Image> _images;
        
        [SerializeField] private Image _activeImage;
        
        #region Constants
        private const float TRANSITION_TIME = 1f;
        #endregion
        
        public void SwitchActive(int id)
        {
            // Deactivate old image
            _activeImage.color = _deactiveColor;
            StartCoroutine(EasedLerpScale(_activeImage.GetComponent<RectTransform>(), false));
            ActivateImage(id);
        }

        protected void ActivateImage(int id)
        {
            // Activate new image
            _activeImage = _images[id];
            _activeImage.color = _activeColor;
            StartCoroutine(EasedLerpScale(_activeImage.GetComponent<RectTransform>(), true));
        }
   
        // I wanted to seperate this function to it's own utilities file, as similar variations in a few places.
        // However, I can't see how I could  get the IEnumerator to return a (i.e. float size) on every loop, which the
        // caller would pick up on. 
        private IEnumerator EasedLerpScale(RectTransform image, bool shouldEnlarge)
        {
            var t = 0f;
            var easedT = 0f;
            while (Mathf.Abs(t) < TRANSITION_TIME)
            {
                easedT = EasingUtils.Back.Out(t);
                var size = (shouldEnlarge ? _minImageSize : _maxImageSize) + easedT * (_maxImageSize - _minImageSize) * (shouldEnlarge ? 1 : -1);
                image.sizeDelta = Vector2.one * size;
                t += Time.deltaTime;
                //yield return null;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

