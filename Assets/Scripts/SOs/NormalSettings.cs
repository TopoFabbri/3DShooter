using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(menuName = "SOs/NormalSettings", fileName = "NormalSettings", order = 0)]
    public class NormalSettings : EnemySettings
    {
        [Header("Normal:")]
        public float damageTime = 2f;
        public float damage = 40f;
        public string characterTag = "Character";
    }
}