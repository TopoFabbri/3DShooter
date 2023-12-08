using Abstracts;
using UnityEngine;

namespace SOs
{
    /// <summary>
    /// Spawnable pickup settings
    /// </summary>
    [CreateAssetMenu(fileName = "_PickupSettings", menuName = "SOs/PickupSettings")]
    public class PickupSettings : SpawnableSettings
    {
        public GameObject lethalPrefab;
        public Mesh mesh;
        public Color lightColor;
    }
}
