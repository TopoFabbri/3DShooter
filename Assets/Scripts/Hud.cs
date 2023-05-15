using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI frames;
    [SerializeField] private Stats stats;

    // Update is called once per frame
    void Update()
    {
        slider.value = stats.GetHp();
        frames.SetText(((int)(1f / Time.deltaTime)).ToString());
    }
}
