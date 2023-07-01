using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BulletsUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private PlayerController player;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private Id stateId;
    
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        stateMachine = FindObjectOfType<StateMachine>();
        stateMachine.Subscribe(stateId, OnUpdate);
    }

    private void OnDisable()
    {
        stateMachine.UnSubscribe(stateId, OnUpdate);
    }
    
/// <summary>
/// Gameplay-only update
/// </summary>
    private void OnUpdate()
    {
        if (player.getWeapon)
        {
            image.color = Color.white;
            image.sprite = sprites[player.getWeapon.chamber];
        }
        else
        {
            image.color = new Color(0f, 0f, 0f, 0f);
        }
    }
}