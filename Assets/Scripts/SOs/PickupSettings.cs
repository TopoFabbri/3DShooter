using Abstracts;
using UnityEngine;

namespace Lethals
{
    [CreateAssetMenu(fileName = "_PickupSettings", menuName = "SOs/PickupSettings")]
    public class PickupSettings : SpawnableSettings
    {
        public GameObject lethalPrefab;
        public Mesh mesh;
        public Color lightColor;
    }
}
