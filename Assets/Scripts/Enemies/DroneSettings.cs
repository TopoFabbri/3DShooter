using System.Collections.Generic;
using SOs;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "SOs/DroneSettings", fileName = "DroneSettings", order = 0)]
    public class DroneSettings : EnemySettings
    {
        public float disToPlayer = 10f;
        public float minMoveTime = 1f;
        public float maxMoveTime = 5f;
        public float flyHeight = 2f;
        public float fireCooldown = 5f;
        public float fireTime = 1f;
        public float paralyzeTime = 3f;
        public float showRayTime = 1f;
    }
}