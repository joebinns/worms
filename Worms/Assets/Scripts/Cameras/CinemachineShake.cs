using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Cameras
{
    public class CinemachineShake : MonoBehaviour
    {
        #region Singleton
        public static CinemachineShake Instance { get; private set; }
        #endregion
        
        [SerializeField] private List<CinemachineVirtualCamera> _virtualCameras;
        [SerializeField] private List<NoiseSettings> _noiseSettings;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void SwitchNoiseProfile(string name)
        {
            var noiseSetting = _noiseSettings.Find(x => x.name == name);
            foreach (var virtualCamera in _virtualCameras)
            {
                virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = noiseSetting;
            }
        }

        private void SetAmplitudeGains(float amplitudeGain)
        {
            foreach (var virtualCamera in _virtualCameras)
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
                SwitchNoiseProfile(_noiseSettings[0].name); // Revert to default noise setting
            }
            yield break;
        }

        public void InvalidInputPresetShake()
        {
            ShakeCamera(2.5f, 0.4f, "1D Wobble");
        }
    
    }
}