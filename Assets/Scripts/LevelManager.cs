using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int devilFrequency = 4;
    [SerializeField] private float spawnCooldown = 5f;
    [SerializeField] private List<GameObject> enemyPrefab = new();
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private List<GameObject> enemies = new();
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private SceneId menu;

    private int spawnedEnemies;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void OnEnable()
    {
        DevilEnemy.DevilDestroyed += OnEnemyDestroyed;
        NormalEnemy.ZombieDestroyed += OnEnemyDestroyed;
        PlayerController.Destroyed += CharacterDestroyed;
    }

    private void OnDisable()
    {
        DevilEnemy.DevilDestroyed -= OnEnemyDestroyed;
        NormalEnemy.ZombieDestroyed -= OnEnemyDestroyed;
        PlayerController.Destroyed -= CharacterDestroyed;
    }

    /// <summary>
    /// Spawns enemies on given time
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator SpawnEnemies()
    {
        var spawnIndex = 0;

        while (gameObject.activeSelf)
        {
            var enemyIndex = 0;

            if (spawnedEnemies > 0 && spawnedEnemies % devilFrequency == 0)
                enemyIndex = 1;

            if (spawnIndex >= spawnPoints.Count)
                spawnIndex = 0;

            var enemy = Instantiate(enemyPrefab[enemyIndex], spawnPoints[spawnIndex].position,
                spawnPoints[spawnIndex].rotation);

            enemies.Add(enemy);

            spawnIndex++;
            spawnedEnemies++;

            yield return new WaitForSeconds(spawnCooldown);

            if (pauseMenu.paused)
                yield return new WaitUntil(() => !pauseMenu.paused);
        }
    }

    /// <summary>
    /// Removes destroyed enemies from list
    /// </summary>
    /// <param name="gObject"></param>
    private void OnEnemyDestroyed(GameObject gObject)
    {
        if (enemies.Contains(gObject))
            enemies.Remove(gObject);
    }

    /// <summary>
    /// Load menu when character dies
    /// </summary>
    private void CharacterDestroyed()
    {
        sceneLoader.LoadScene(menu);
    }
}