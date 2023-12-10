using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(menuName = "SOs/DevilSettings", fileName = "DevilSettings", order = 0)]
    public class DevilSettings : EnemySettings
    {
        [Header("Devil:")]
        public float fireDis = 8f;
        public float cooldown = 1f;
        public float longCooldown = 5f;
        public GameObject fireBall;
    }
}