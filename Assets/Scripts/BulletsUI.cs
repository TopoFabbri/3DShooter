using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private PlayerController player;

    //TODO: Fix - Add [RequireComponentAttribute]
    private Image image;
    
    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
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