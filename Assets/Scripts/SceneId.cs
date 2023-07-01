using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneId_", menuName = "Data/SceneId")]
public class SceneId : ScriptableObject
{
    public string id => name;
}
