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
    public string suggestedName { get; set; }
    public HatSettings hat { get; set; }
    public bool shouldSpawn { get; set; }

    public void OnEnable() // Why is this being called spuradically? Because the scriptable object is going "out of scope"... meaning it is no longer referenced...
    { // https://forum.unity.com/threads/scriptableobject-behaviour-discussion-how-scriptable-objects-work.541212/
        //RestoreDefaults();
    }

    // Restore the scriptable object values to base values at runtime.
    public void RestoreDefaults()
    {
        id = baseId;
        name = baseName;
        hat = baseHat;
        suggestedName = baseSuggestedName;
        shouldSpawn = baseShouldSpawn;
    }

}
