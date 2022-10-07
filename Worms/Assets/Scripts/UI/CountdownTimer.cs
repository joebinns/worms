using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CountdownTimer : MonoBehaviour
    {	
        [SerializeField] private TMP_Text _text; 
        private bool _isPlaying = true;
        private float _timer;

        #region Constants
        private const float MAX_TIME = 31f;
        private const float FLASH_MULTIPLIER = 0.1f;
        #endregion

        #region Events
        public static event Action OnCountedDown;
        #endregion

        private void Awake()
        {
            ResetTimer();
        }

        private void Update()
        {
            if(_isPlaying == true)
            {
                _timer -= Time.deltaTime;
                _text.text = ((int)_timer).ToString();
                if (_timer <= 10f)
                {
                    var rate = (MAX_TIME - _timer);
                    var alpha = (Mathf.Pow(rate, 1.5f) * FLASH_MULTIPLIER) % 1f;
                    _text.alpha = alpha;
                }
            }
            if (_timer <= 0f)
            {
                // Next Turn
                ResetTimer();
                OnCountedDown?.Invoke();
            }
        }

        // Make text flash faster as timer gets lower
        public void ResetTimer()
        {
            _timer = MAX_TIME;
            _text.alpha = 1f;
        }
    }
}