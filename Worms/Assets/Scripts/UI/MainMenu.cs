using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.Instance.Play("Click Secondary");
        LoadingScreen.Instance.ChangeSceneImpatient(SceneIndices.PLAYER_SELECT);
    }
    
    public void ExitGame()
    {
        AudioManager.Instance.Play("Click Secondary");
        Application.Quit();
    }
    
}
