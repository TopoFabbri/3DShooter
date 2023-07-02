using System.Collections;
using UnityEngine;

public class GameTimeCounter : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    
    private const int SecondsToTimeUnit = 1;
    public int gameTime { get; private set; }

    private void Awake()
    {
        StartCoroutine(CountGameTime());
    }

    private IEnumerator CountGameTime()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(SecondsToTimeUnit);
            gameTime++;

            if (pauseMenu.paused)
                yield return new WaitUntil(() => !pauseMenu.paused);
        }
    }
}
