using UnityEngine;

public class VictoryScene : MonoBehaviour
{
    private void Start()
    {
        CursorVisibilityToggle.EnableCursor();
        Cursor.lockState = CursorLockMode.None;
        
        AudioManager.Instance.PlayDelayed("Celebration", 0.5f);
    }

    public void ReturnToMainMenu()
    {
        AudioManager.Instance.Play("Click Secondary");
        StartCoroutine(LoadingScreen.ChangeSceneImpatient(SceneIndices.MAIN_MENU));
    }
    
}
