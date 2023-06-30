using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI frames;
    [SerializeField] private GameObject pickUpTxt;

    private void Update()
    {
        frames.SetText(((int)(1f / Time.deltaTime)).ToString());
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
    public void SetTextActive(bool active)
    {
        pickUpTxt.SetActive(active);
    }
}
