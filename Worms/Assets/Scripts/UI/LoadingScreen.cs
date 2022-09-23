using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private static Animator _transition;
    private const float TRANSITION_TIME = 0.16666666666f;

    public static event Action OnTransitionedToLoadingScreen;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        _transition = GetComponent<Animator>();
    }

    public static IEnumerator TransitionToLoadingScreen()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(TRANSITION_TIME);
        OnTransitionedToLoadingScreen?.Invoke();
    }

    public static void TransitionFromLoadingScreen()
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
