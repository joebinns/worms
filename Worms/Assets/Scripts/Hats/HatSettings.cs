using UnityEngine;

[CreateAssetMenu(fileName = "HatSettings", menuName = "ScriptableObjects/Hat")]
public class HatSettings : ScriptableObject
{
    public int id;
    public new string name;
    public GameObject prefab;

}
