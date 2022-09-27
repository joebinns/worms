using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/Player")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private int baseId;
    [SerializeField] private string baseName;
    [SerializeField] private string baseSuggestedName;
    [SerializeField] private HatSettings baseHat;
    [SerializeField] private bool baseShouldSpawn;

    public int id { get; set; }
    public new string name { get; set; }
    public new string suggestedName { get; set; }
    public HatSettings hat { get; set; }
    public bool shouldSpawn { get; set; }

    public void OnEnable()
    {
        RestoreDefaults();
    }

    // Restore the scriptable object values to base values at runtime.
    private void RestoreDefaults()
    {
        id = baseId;
        name = baseName;
        hat = baseHat;
        suggestedName = baseSuggestedName;
        shouldSpawn = baseShouldSpawn;
    }

}
