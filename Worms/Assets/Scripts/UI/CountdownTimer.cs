using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CountdownTimer : MonoBehaviour
{	
    [SerializeField] private TMP_Text text; 
    private bool isPlaying = true;
    private float _timer;

    private const float MAX_TIME = 16f;
    private const float FLASH_MULTIPLIER = 0.175f;

    public static event Action OnCountedDown;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if(isPlaying == true)
        {
            _timer -= Time.deltaTime;
            text.text = ((int)_timer).ToString();

            if (_timer <= 10f)
            {
                var rate = (MAX_TIME - _timer);
                var alpha = (rate * rate * FLASH_MULTIPLIER) % 1f;
                text.alpha = alpha;
            }
        }

        if (_timer <= 0)
        {
            // Next Turn
            OnCountedDown?.Invoke();
        }

    }

    // Make text flash faster as timer gets lower
    public void ResetTimer()
    {
        _timer = MAX_TIME;
        text.alpha = 1f;
    }

}