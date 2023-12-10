using Abstracts;
using UnityEngine;

namespace SOs
{
    public abstract class EnemySettings : SpawnableSettings
    {
        [Header("Enemy:")]
        public float speed = 20f;
        public float maxSpeed = 2f;
        public string characterName = "Character";

        public Transform target;
    }
}