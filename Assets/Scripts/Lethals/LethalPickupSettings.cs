using UnityEngine;

namespace Lethals
{
    [CreateAssetMenu(fileName = "_PickupSettings", menuName = "SOs/LethalPickupSettings")]
    public class LethalPickupSettings : ScriptableObject
    {
        public GameObject lethalPrefab;
        public Mesh mesh;
        public Color lightColor;
    }
}
