using Audio;
using UnityEngine;

namespace UI
{
    public class VictoryScene : MonoBehaviour
    {
        private void Start()
        {
            CursorMode.EnableCursor();
        
            AudioManager.Instance.PlayDelayed("Celebration", 0.5f);
        }

        public void ReturnToMainMenu()
        {
            AudioManager.Instance.Play("Click Secondary");
            LoadingScreen.Instance.ChangeSceneImpatient(SceneIndices.MAIN_MENU);
        }
    
    }
}
