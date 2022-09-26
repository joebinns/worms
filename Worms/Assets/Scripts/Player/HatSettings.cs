using UnityEngine;

[CreateAssetMenu(fileName = "HatSettings", menuName = "ScriptableObjects/Hat")]
public class HatSettings : ScriptableObject // Not sure why this is not working...
{
    public int id;
    public new string name;
    public GameObject prefab;

}
