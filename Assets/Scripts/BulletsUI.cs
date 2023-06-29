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
        stateMachine = FindObjectOfType<StateMachine>();
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
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
        if (player.GetWeapon())
        {
            image.color = Color.white;
            image.sprite = sprites[player.GetWeapon().GetBullets()];
        }
        else
        {
            image.color = new Color(0f, 0f, 0f, 0f);
        }
    }
}