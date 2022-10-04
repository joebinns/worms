using UnityEngine;

public class VictoryScene : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayDelayed("Celebration", 0.8f);
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadingScreen.ChangeSceneImpatient(SceneIndices.MAIN_MENU));
    }
    
}
