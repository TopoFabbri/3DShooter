using System.Collections;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Hud hud;
    [SerializeField] private int secondsToScore = 10;
    [SerializeField] private int scorePerTime = 10;

    private int score;

    private void Start()
    {
        Stats.DestroyedEvent += OnEnemyDestroyed;
        StartCoroutine(TimeScore());
    }

    private IEnumerator TimeScore()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(secondsToScore);
            score += scorePerTime;
            hud.UpdateScore(score);

            if (pauseMenu.paused)
                yield return new WaitUntil(() => !pauseMenu.paused);
        }
    }

    private void OnEnemyDestroyed(GameObject gObject)
    {
        score++;
    }
}