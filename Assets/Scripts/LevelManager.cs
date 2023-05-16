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
    [SerializeField] private List<GameObject> enemyPrefab = new List<GameObject>();
    [SerializeField] private List<Transform> spawns = new List<Transform>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    private int spawnIndex = 0;
    private float spawnTime = 0;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
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
        
        enemies.Add(Instantiate(enemyPrefab[i], spawns[spawnIndex].position, spawns[spawnIndex].rotation));

        spawnIndex++;
        spawnTime = Time.time + spawnCooldown;
    }
}
