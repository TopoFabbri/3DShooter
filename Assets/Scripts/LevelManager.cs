using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int devilDensity = 4;
    [SerializeField] private float spawnCooldown = 5f;
    [SerializeField] private List<GameObject> enemyPrefab = new();
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private List<GameObject> enemies = new();
    [SerializeField] private PauseMenu pauseMenu;

    private void Start()
    {
        Stats.DestroyedEvent += OnEnemyDestroyed;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        StartCoroutine(SpawnEnemies());
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

            if (enemies.Count > 0 && enemies.Count % devilDensity == 0)
                enemyIndex = 1;

            if (spawnIndex >= spawnPoints.Count)
                spawnIndex = 0;

            var enemy = Instantiate(enemyPrefab[enemyIndex], spawnPoints[spawnIndex].position,
                spawnPoints[spawnIndex].rotation);

            enemies.Add(enemy);

            spawnIndex++;

            yield return new WaitForSeconds(spawnCooldown);

            if (pauseMenu.paused)
                yield return new WaitUntil(() => !pauseMenu.paused);
        }
    }

    private void OnEnemyDestroyed(GameObject gObject)
    {
        if (enemies.Contains(gObject))
            enemies.Remove(gObject);
    }
}