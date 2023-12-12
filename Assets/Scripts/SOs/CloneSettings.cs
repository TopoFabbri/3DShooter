using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(menuName = "SOs/CloneSettings", fileName = "CloneSettings", order = 0)]
    public class CloneSettings : EnemySettings
    {
        [Header("Clone:")] 
        public float shootDis = 10f;
        public float shootSpread = 5f;
    }
}