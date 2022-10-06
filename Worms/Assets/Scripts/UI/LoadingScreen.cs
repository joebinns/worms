using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace UI
{
    public class LoadingScreen : MonoBehaviour
    {
        #region Singleton
        public static LoadingScreen Instance;
        #endregion
        
        private Animator _transition;
        private const float TRANSITION_TIME = 0.16666666666f;
        
        #region Events (Now redundant)
        public event Action OnTransitionedToLoadingScreen;
        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
        
            DontDestroyOnLoad(Instance);
            _transition = GetComponent<Animator>();
        }

        public void TransitionToLoadingScreen()
        {
            StartCoroutine(TransitionToLoadingScreenCoroutine());
        }

        private IEnumerator TransitionToLoadingScreenCoroutine()
        {
            _transition.SetTrigger("Start");
            yield return new WaitForSeconds(TRANSITION_TIME);
        
            OnTransitionedToLoadingScreen?.Invoke();
        }

        public void TransitionFromLoadingScreen(SceneIndices index)
        {
            // Load scene
            LoadScene(index);
            // Reveal new scene
            _transition.SetTrigger("End");
        }

        private void LoadScene(SceneIndices index)
        {
            SceneManager.LoadScene((int)index);
        }

        public void ChangeSceneImpatient(SceneIndices index)
        {
            StartCoroutine(ChangeSceneImpatientCoroutine(index));
        }
    
        /// <summary>
        /// For changing scenes, without needing to take precaution to make time for any methods invoked by OnTransitionedToLoadingScreen 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private IEnumerator ChangeSceneImpatientCoroutine(SceneIndices index)
        {
            yield return TransitionToLoadingScreenCoroutine();
            TransitionFromLoadingScreen(index);
        }

    }
}
