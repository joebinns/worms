using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Nameplate : MonoBehaviour
{
    private const float DEFAULT_ALPHA = 255f/100f;

    private CanvasGroup _canvasGroup;
    
    private Player.Player _player;

    [SerializeField] private Transform _nameplate;
    [SerializeField] private Transform _knockbackplate;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _player = transform.parent.GetComponent<FollowPosition>().target.GetComponent<Player.Player>();
    }

    public void ChangeName(string name)
    {
        var textplateSizer = _nameplate.GetComponent<TextSizer>();
        textplateSizer.Text.text = name;
        textplateSizer.Refresh();
    }

    public void ChangeKnockback(int knockback)
    {
        var knockbackplateSizer = _knockbackplate.GetComponent<TextSizer>();
        knockbackplateSizer.Text.text = knockback.ToString() + "<size=85%><u><voffset=.125em>%</voffset></u>";
        knockbackplateSizer.Text.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(255, 0, 0, 255), (float)knockback/(float)200);
        knockbackplateSizer.Refresh();
    }

    private void OnEnable()
    {
        CameraManager.OnCameraStateChanged += ChangeVisibility;
        _player.GetComponent<Knockback>().OnKnockbackChanged += ChangeKnockback;
    }

    private void OnDisable()
    {
        CameraManager.OnCameraStateChanged -= ChangeVisibility;
        _player.GetComponent<Knockback>().OnKnockbackChanged -= ChangeKnockback;
    }

    private void ChangeVisibility(CameraState cameraState)
    {
        if (PlayerManager.Instance.currentPlayer != _player)
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
