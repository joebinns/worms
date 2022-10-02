using UnityEngine;

public class VictoryScene : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadingScreen.ChangeSceneImpatient(SceneIndices.MAIN_MENU));
    }
    
}
