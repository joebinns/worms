using Items.Hats;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/Player")]
    public class PlayerSettings : ScriptableObject
    {
        #region Impermutable
        [SerializeField] private int _id;
        public int ID => _id;
        [SerializeField] private string _suggestedName;
        public string SuggestedName => _suggestedName;
        #endregion
        
        #region Permutable
        // Save the default player settings between runs
        [SerializeField] private string _defaultName;
        [SerializeField] private HatSettings _defaultHat;
        [SerializeField] private bool _defaultShouldSpawn = true;
        
        public new string name { get; set; }
        public HatSettings hat { get; set; }
        public bool shouldSpawn { get; set; }
        #endregion
        
        // Restore the scriptable object's permutable values to their persistent default values on awake of the player 
        // select scene.
        // NOTE: RestoreDefaults() should not be called on this scriptable objects OnEnable, as OnEnable may be called 
        // sporadically as PlayerSettings moves in and out of scope.
        public void RestoreDefaults()
        {
            name = _defaultName;
            hat = _defaultHat;
            shouldSpawn = _defaultShouldSpawn;
        }

    }
}
