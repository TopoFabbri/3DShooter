using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int[] devilFrequency;
    [SerializeField] private float[] spawnCooldown;
    [SerializeField] private int[] maxEnemies;
    [SerializeField] private List<GameObject> enemyPrefab = new();
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private List<GameObject> enemies = new();
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private SceneId menu;
    [SerializeField] private SceneId credits;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseScreen;
    
    public int enemyCount => enemies.Count;

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
        RoomManager.OnRoomChanged += OnRoomChanged;
    }

    private void OnDisable()
    {
        DevilEnemy.DevilDestroyed -= OnEnemyDestroyed;
        NormalEnemy.ZombieDestroyed -= OnEnemyDestroyed;
        PlayerController.Destroyed -= CharacterDestroyed;
        RoomManager.OnRoomChanged -= OnRoomChanged;
    }

    /// <summary>
    /// Spawns enemies on given time
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator SpawnEnemies()
    {
        var spawnIndex = 0;

        while (spawnedEnemies < maxEnemies[roomManager.current])
        {
            var enemyIndex = 0;

            if (spawnedEnemies > 0 && spawnedEnemies % devilFrequency[roomManager.current] == 0)
                enemyIndex = 1;

            if (spawnIndex >= spawnPoints.Count)
                spawnIndex = 0;

            var enemy = Instantiate(enemyPrefab[enemyIndex], spawnPoints[spawnIndex].position,
                spawnPoints[spawnIndex].rotation);

            enemies.Add(enemy);

            spawnIndex++;
            spawnedEnemies++;

            yield return new WaitForSeconds(spawnCooldown[roomManager.current]);

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

    /// <summary>
    /// Call action player entered next room
    /// </summary>
    /// <param name="newSpawns"></param>
    private void OnRoomChanged(List<Transform> newSpawns)
    {
        spawnPoints.Clear();
        spawnPoints = newSpawns;
        spawnedEnemies = 0;
        
        StartCoroutine(SpawnEnemies());
    }

    /// <summary>
    /// Activate win canvas
    /// </summary>
    public void ShowWinScreen()
    {
        winCanvas.SetActive(true);
    }

    /// <summary>
    /// Start event player lose
    /// </summary>
    public void Lose()
    {
        StartCoroutine(WaitAndChangeScene(1, credits));
    }

    /// <summary>
    /// Change scene on given time
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="sceneId"></param>
    /// <returns></returns>
    private IEnumerator WaitAndChangeScene(int seconds, [NotNull] SceneId sceneId)
    {
        if (!sceneId) throw new ArgumentNullException(nameof(sceneId));
        
        loseScreen.SetActive(true);
        
        yield return new WaitForSeconds(seconds);
        
        if (sceneLoader)
            sceneLoader.LoadScene(sceneId);
    }
}