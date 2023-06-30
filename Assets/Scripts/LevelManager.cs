using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [Header("Enemies:")]
    [SerializeField] private int maxEnemies = 20;
    [SerializeField] private int devilDensity = 4;
    [SerializeField] private float spawnCooldown = 5f;
    [SerializeField] private List<GameObject> enemyPrefab = new();
    [SerializeField] private List<Transform> spawns = new();
    [SerializeField] private List<GameObject> enemies = new();
    [SerializeField] private Transform characterTransform;
    [SerializeField] private StateMachine stateMachine;

    private int spawnIndex;
    private float spawnTime;

    private void Start()
    {
        Stats.DestroyedEvent += OnEnemyDestroyed;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        //TODO: Fix - Could be a coroutine
        if (Time.time > spawnTime && enemies.Count < maxEnemies)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        int i = 0;

        if (enemies.Count > 0 && enemies.Count % devilDensity == 0)
            i = 1;

        if (spawnIndex >= spawns.Count)
            spawnIndex = 0;

        var enemy = Instantiate(enemyPrefab[i], spawns[spawnIndex].position, spawns[spawnIndex].rotation);

        enemies.Add(enemy);

        spawnIndex++;
        spawnTime = Time.time + spawnCooldown;
    }

    private void OnEnemyDestroyed(GameObject gObject)
    {
        if (enemies.Contains(gObject))
            enemies.Remove(gObject);
    }
}
