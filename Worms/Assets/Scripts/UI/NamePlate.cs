using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NamePlate : MonoBehaviour
{
    private const float DEFAULT_ALPHA = 255f/100f;

    private CanvasGroup _canvasGroup;
    
    private Player _player;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _player = transform.parent.GetComponent<FollowPosition>().target.GetComponent<Player>();
    }

    private void OnEnable()
    {
        CameraManager.OnCameraStateChanged += ChangeVisibility;
    }

    private void OnDisable()
    {
        CameraManager.OnCameraStateChanged -= ChangeVisibility;
    }

    private void ChangeVisibility(CameraState cameraState)
    {
        if (PlayerManager.currentPlayer != _player)
        {
            return;
        }
        
        if (cameraState == CameraState.AimCamera)
        {
            StartCoroutine(Hide(0.5f));
        }
        else if (cameraState == CameraState.FollowCamera)
        {
            StartCoroutine(Show(0.5f));
        }
    }

    private IEnumerator Hide(float duration)
    {
        var t = 0f;
        while (t < duration)
        {
            _canvasGroup.alpha = 1f - (t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        _canvasGroup.alpha = 0f;
    }
    
    private IEnumerator Show(float duration)
    {
        var t = 0f;
        while (t < duration)
        {
            _canvasGroup.alpha = t / duration;
            t += Time.deltaTime;
            yield return null;
        }
        _canvasGroup.alpha = DEFAULT_ALPHA;
    }
}
