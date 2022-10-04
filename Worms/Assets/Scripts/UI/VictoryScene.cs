using UnityEngine;

public class VictoryScene : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayDelayed("Celebration", 0.8f);
    }

    public void ReturnToMainMenu()
    {
        AudioManager.Instance.Play("Click Secondary");
        StartCoroutine(LoadingScreen.ChangeSceneImpatient(SceneIndices.MAIN_MENU));
    }
    
}
