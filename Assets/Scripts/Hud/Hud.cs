using System.Collections;
using Character;
using GameStats;
using Patterns;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace HUD
{
    /// <summary>
    /// Hud controller class
    /// </summary>
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI dropLethalText;
        [SerializeField] private TextMeshProUGUI inventoryText;
        [SerializeField] private GameObject pickupText;
        [SerializeField] private GameObject paralyzedText;
        [SerializeField] private GameTimeCounter gameTimer;
        [SerializeField] private float barrelTutorialMaxTime = 10f;
        
        private bool barrelTutorialCompleted;
        private const string timeLabel = "Time: ";

        private void OnEnable()
        {
            InputListener.DropLethal += OnLethalUsed;
        }

        private void OnDisable()
        {
            InputListener.DropLethal -= OnLethalUsed;
        }

        private void Update()
        {
            if (barrelTutorialCompleted || PlayerInventory.Instance.lethals.Count <= 0)
            {
                dropLethalText.gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(EndBarrelTutorial(barrelTutorialMaxTime));
                dropLethalText.gameObject.SetActive(true);
            }

            fpsText.SetText(((int)(1f / Time.deltaTime)).ToString());
            timeText.SetText(timeLabel + gameTimer.GameTime);
            inventoryText.SetText(PlayerInventory.Text);
        }

        /// <summary>
        /// Set slider value based on hp
        /// </summary>
        /// <param name="currentHp"></param>
        public void SetHealthSlider(float currentHp)
        {
            healthSlider.value = currentHp;
        }

        /// <summary>
        /// Show or hide 'pickup' text
        /// </summary>
        /// <param name="isActive"></param>
        public void SetPickupTextActive(bool isActive)
        {
            pickupText.SetActive(isActive && barrelTutorialCompleted);
        }
        
        /// <summary>
        /// Show or hide 'paralyzed' text
        /// </summary>
        /// <param name="isParalyzed">True to show false to hide</param>
        public void SetParalyzedHud(bool isParalyzed)
        {
            pickupText.SetActive(!isParalyzed);
            barrelTutorialCompleted = isParalyzed || barrelTutorialCompleted;
            paralyzedText.SetActive(isParalyzed);
        }

        /// <summary>
        /// Update score text
        /// </summary>
        /// <param name="newScore"></param>
        public void UpdateScoreText(int newScore)
        {
            scoreText.SetText(newScore.ToString());
        }

        /// <summary>
        /// Stop barrel tutorial on barrel used
        /// </summary>
        private void OnLethalUsed()
        {
            barrelTutorialCompleted = true;
        }

        /// <summary>
        /// Wait time and close barrel tutorial
        /// </summary>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        private IEnumerator EndBarrelTutorial(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            barrelTutorialCompleted = true;
        }
    }
}
