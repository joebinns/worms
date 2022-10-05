using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras;

    [SerializeField] private List<NoiseSettings> noiseSettings;

    private void Awake()
    {
        Instance = this;
    }

    private void SwitchNoiseProfile(string name)
    {
        var noiseSetting = noiseSettings.Find(x => x.name == name);

        foreach (var virtualCamera in virtualCameras)
        {
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = noiseSetting;
        }
    }

    private void SetAmplitudeGains(float amplitudeGain)
    {
        foreach (var virtualCamera in virtualCameras)
        {
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
        }
    }

    public void ShakeCamera(float intensity = 2.5f, float duration = 0.4f, string overrideNoiseProfile = null)
    {
        StartCoroutine(Instance.ShakeCameraCoroutine(intensity, duration,overrideNoiseProfile));
    }

    private IEnumerator ShakeCameraCoroutine(float intensity = 2.5f, float duration = 0.4f, string overrideNoiseProfile = null)
    {
        if (overrideNoiseProfile != null)
        {
            SwitchNoiseProfile(overrideNoiseProfile);
        }

        var t = duration;
        while (t > 0f)
        {
            SetAmplitudeGains(Mathf.Lerp(intensity, 0f, 1 - (t / duration)));

            t -= Time.deltaTime;
            yield return null;
        }

        SetAmplitudeGains(0f);

        if (overrideNoiseProfile != null)
        {
            SwitchNoiseProfile(noiseSettings[0].name); // Revert to default noise setting
        }
        yield break;
    }

    public void InvalidInputPresetShake()
    {
        ShakeCamera(2.5f, 0.4f, "1D Wobble");
    }
    
}