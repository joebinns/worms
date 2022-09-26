using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/Player")]
public class PlayerSettings : ScriptableObject
{
    public new string name;
    public HatSettings hat;
}
