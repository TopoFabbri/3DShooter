using System.Collections;
using Enemies;
using Menus;
using UnityEngine;

namespace Level
{
    /// <summary>
    /// Score related actions
    /// </summary>
    public class ScoringSystem : MonoBehaviour
    {
        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private HUD.Hud hud;
        [SerializeField] private int secondsToScore = 10;
        [SerializeField] private int scorePerTime = 10;

        private int score;

        private void Start()
        {
            StartCoroutine(TimeScore());
        }

        private void OnEnable()
        {
            DevilEnemy.DevilDestroyed += OnEnemyDestroyed;
            NormalEnemy.ZombieDestroyed += OnEnemyDestroyed;
        }

        private void OnDisable()
        {
            DevilEnemy.DevilDestroyed -= OnEnemyDestroyed;
            NormalEnemy.ZombieDestroyed -= OnEnemyDestroyed;
        }

        /// <summary>
        /// Increase score by time
        /// </summary>
        /// <returns></returns>
        private IEnumerator TimeScore()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(secondsToScore);
                score += scorePerTime;
                hud.UpdateScoreText(score);

                if (pauseMenu.paused)
                    yield return new WaitUntil(() => !pauseMenu.paused);
            }
        }

        /// <summary>
        /// Increase score by destroying enemies
        /// </summary>
        /// <param name="gObject">Enemy</param>
        private void OnEnemyDestroyed(GameObject gObject)
        {
            score++;
            hud.UpdateScoreText(score);
        }
    }
}