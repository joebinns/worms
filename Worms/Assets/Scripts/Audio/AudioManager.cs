using System;
using System.Collections;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// An Audio Manager. Based on Brackey's tutorial https://youtu.be/6OT43pvUyfY.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public Sound[] Sounds;

        /// <summary>
        /// Declare the properties of the sounds.
        /// </summary>
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            foreach (Sound s in Sounds)
            {
                s.Source = gameObject.AddComponent<AudioSource>();
                s.Source.clip = s.Clip;
                s.Source.playOnAwake = false;

                s.Source.volume = s.Volume;
                s.Source.pitch = s.Pitch;
                s.Source.loop = s.Loop;
            }
        }

        /// <summary>
        /// Update the audio clips properties of the sounds.
        /// </summary>
        private void Update()
        {
            foreach (Sound s in Sounds)
            {
                s.Source.volume = s.Volume;
                s.Source.pitch = s.Pitch;
                s.Source.loop = s.Loop;
            }
        }

        /// <summary>
        /// Beginning playing a chosen sound.
        /// </summary>
        /// <param name="name">The name of the sound.</param>
        public void Play(string name)
        {
            Sound s = Array.Find(Sounds, sound => sound.Name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }

            s.Source.Play();
        }

        public void PlayDelayed(string name, float delay)
        {
            StartCoroutine(PlayDelayedCoroutine(name, delay));
        }

        private IEnumerator PlayDelayedCoroutine(string name, float delay)
        {
            yield return new WaitForSeconds(delay);
            Play(name);
        }

        /// <summary>
        /// Get whether a sound is playing or not.
        /// </summary>
        /// <param name="name">The name of the sound.</param>
        /// <returns>Whether the sound is playing or not.</returns>
        public bool IsPlaying(string name)
        {
            Sound s = Array.Find(Sounds, sound => sound.Name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return false;
            }

            return(s.Source.isPlaying);
        }

        /// <summary>
        /// Play two sounds consecutively.
        /// </summary>
        /// <param name="name1">The name of the first sound to be played.</param>
        /// <param name="name2">The name of the sound to be player after the first has finished.</param>
        /// <returns></returns>
        public IEnumerator PlayQueued(string name1, string name2)
        {
            Sound s1 = Array.Find(Sounds, sound => sound.Name == name1);
            Sound s2 = Array.Find(Sounds, sound => sound.Name == name2);
            if (s1 == null)
            {
                Debug.LogWarning("Sound: " + name1 + " not found");
                yield return null;
            }
            if (s2 == null)
            {
                Debug.LogWarning("Sound: " + name2 + " not found");
                yield return null;
            }

            if (s1.Source.isPlaying | s2.Source.isPlaying)
            {
                yield return null;
            }

            s1.Source.Play();
            yield return new WaitForSeconds(s1.Clip.length);
            s2.Source.Play();
        }

        /// <summary>
        /// Stop playing a sound.
        /// </summary>
        /// <param name="name">The name of the sound.</param>
        public void Stop(string name)
        {
            Sound s = Array.Find(Sounds, sound => sound.Name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }

            s.Source.Stop();
        }
    }
}