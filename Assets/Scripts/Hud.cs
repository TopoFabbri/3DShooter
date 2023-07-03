using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI frames;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI dropLethal;
    [SerializeField] private GameObject pickUpTxt;
    [SerializeField] private GameTimeCounter gameTimeCounter;

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
        if (barrelTutorialFinished)
            dropLethal.gameObject.SetActive(false);
        
        frames.SetText(((int)(1f / Time.deltaTime)).ToString());
        time.SetText(TimeText + gameTimeCounter.gameTime);
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

    public void UpdateScore(int newScore)
    {
        score.SetText(newScore.ToString());
    }

    private void OnDropLethal()
    {
        barrelTutorialFinished = true;
    }
}
