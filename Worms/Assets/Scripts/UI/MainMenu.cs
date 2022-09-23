using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(LoadingScreen.ChangeSceneImpatient(SceneIndices.PLAYER_SELECT));
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
