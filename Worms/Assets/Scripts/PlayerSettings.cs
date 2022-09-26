using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/Player")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private int baseId;
    [SerializeField] private string baseName;
    [SerializeField] private HatSettings baseHat;

    public int id { get; set; }
    public new string name { get; set; }
    public HatSettings hat { get; set; }

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
    }

}
