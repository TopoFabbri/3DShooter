using Abstracts;
using UnityEngine;
using UnityEngine.Serialization;

namespace SOs
{
    public abstract class EnemySettings : SpawnableSettings
    {
        [Header("Enemy:")]
        public float speed = 20f;
        public float maxSpeed = 2f;
        public string characterName = "Character";
        public int score = 10;
        [FormerlySerializedAs("playSoundEventName")] public string playSoundEvent = "PlayEnemyLoop";
        [FormerlySerializedAs("stopSoundEventName")] public string stopSoundEvent = "StopEnemyLoop";

        public Transform target;
    }
}