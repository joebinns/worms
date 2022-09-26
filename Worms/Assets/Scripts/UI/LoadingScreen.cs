using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;

    private static Animator _transition;
    private const float TRANSITION_TIME = 0.16666666666f;

    public static event Action OnTransitionedToLoadingScreen;

    public void Awake()
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
        
        DontDestroyOnLoad(this);
        _transition = GetComponent<Animator>();
    }

    public static IEnumerator TransitionToLoadingScreen() // It would be a nice touch if the camera zoomed out
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(TRANSITION_TIME);
        OnTransitionedToLoadingScreen?.Invoke();
    }

    public static void TransitionFromLoadingScreen() // It would be a nice touch if the camera zoomed in
    {
        _transition.SetTrigger("End");
    }

    public static void LoadScene(SceneIndices index)
    {
        SceneManager.LoadScene((int)index);
    }

    public static IEnumerator ChangeSceneImpatient(SceneIndices index) // For changing scenes, without needing to take precaution to make time for any methods invoked by OnTransitionedToLoadingScreen 
    {
        yield return TransitionToLoadingScreen();
        LoadScene(index);
        TransitionFromLoadingScreen();
    }

}
