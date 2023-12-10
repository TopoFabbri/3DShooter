using Abstracts;
using UnityEngine;

namespace SOs
{
    /// <summary>
    /// Spawnable pickup settings
    /// </summary>
    [CreateAssetMenu(fileName = "_PickupSettings", menuName = "SOs/PickupSettings", order = 0)]
    public class PickupSettings : SpawnableSettings
    {
        public GameObject prefab;
        public GameObject previewModel;
        public Color lightColor;
    }
}
