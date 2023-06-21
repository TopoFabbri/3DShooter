using UnityEngine;

[CreateAssetMenu(fileName = "Id_", menuName = "Data/ID")]
public class Id : ScriptableObject
{
    public string id => name;
}
