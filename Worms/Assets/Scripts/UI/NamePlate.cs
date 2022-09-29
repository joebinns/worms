using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Nameplate : MonoBehaviour
{
    private const float DEFAULT_ALPHA = 255f/100f;

    private CanvasGroup _canvasGroup;
    
    private Player _player;

    [SerializeField] private Transform _nameplate;
    [SerializeField] private Transform _healthplate;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _player = transform.parent.GetComponent<FollowPosition>().target.GetComponent<Player>();
    }

    public void ChangeName(string name)
    {
        var textplateSizer = _nameplate.GetComponent<TextSizer>();
        textplateSizer.Text.text = name;
        textplateSizer.Refresh();
    }

    public void ChangeHealth(int health)
    {
        var healthplateSizer = _healthplate.GetComponent<TextSizer>();
        healthplateSizer.Text.text = name;
        healthplateSizer.Refresh();
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
            StartCoroutine(Hide());
        }
        else if (cameraState == CameraState.FollowCamera)
        {
            StartCoroutine(Show());
        }
    }

    public IEnumerator Hide(float duration = 0.5f)
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
    
    public IEnumerator Show(float duration = 0.5f)
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
