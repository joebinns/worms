using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HatScriptableObject", order = 1)]
public class HatScriptableObject : ScriptableObject
{
    public string hatName;

    public GameObject hatPrefab;
}
