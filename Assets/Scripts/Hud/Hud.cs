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
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI frames;
        [FormerlySerializedAs("time")] [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI dropLethal;
        [SerializeField] private TextMeshProUGUI inventoryText;
        [SerializeField] private GameObject pickUpTxt;
        [SerializeField] private GameTimeCounter gameTimeCounter;
        [SerializeField] private float barrelTutorialDuration = 10f;
        
        private bool barrelTutorialFinished;
        private const string TimeText = "Time: ";

        private void OnEnable()
        {
            InputListener.DropLethal += OnDropLethal;
        }

        private void OnDisable()
        {
            InputListener.DropLethal -= OnDropLethal;
        }

        private void Update()
        {
            if (barrelTutorialFinished || PlayerInventory.Instance.lethals.Count <= 0)
            {
                dropLethal.gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(StopBarrelTutorial(barrelTutorialDuration));
                dropLethal.gameObject.SetActive(true);
            }

            frames.SetText(((int)(1f / Time.deltaTime)).ToString());
            timeText.SetText(TimeText + gameTimeCounter.GameTime);
            inventoryText.SetText(PlayerInventory.Text);
        }

        /// <summary>
        /// Set slider value based on hp
        /// </summary>
        /// <param name="hp"></param>
        public void SetSlider(float hp)
        {
            slider.value = hp;
        }

        /// <summary>
        /// Show or hide 'pickup' text
        /// </summary>
        /// <param name="active"></param>
        public void SetPickupTextActive(bool active)
        {
            pickUpTxt.SetActive(active && barrelTutorialFinished);
        }

        /// <summary>
        /// Update score text
        /// </summary>
        /// <param name="newScore">New score to display</param>
        public void UpdateScore(int newScore)
        {
            score.SetText(newScore.ToString());
        }

        /// <summary>
        /// Stop barrel tutorial on barrel placed
        /// </summary>
        private void OnDropLethal()
        {
            barrelTutorialFinished = true;
        }

        /// <summary>
        /// Wait time and close barrel tutorial
        /// </summary>
        /// <param name="time">Time to wait</param>
        /// <returns></returns>
        private IEnumerator StopBarrelTutorial(float time)
        {
            yield return new WaitForSeconds(time);
            barrelTutorialFinished = true;
        }
    }
}