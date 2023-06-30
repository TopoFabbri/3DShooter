using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float hp = 100f;
    [SerializeField] private bool isCharacter;
    [SerializeField] private Transform life;
    [SerializeField] private Hud hud;

    private const float LifeRegen = .5f;
    
    public delegate void ObjectDestroyed(GameObject destroyed);
    public static event ObjectDestroyed DestroyedEvent;

    private void Update()
    {
        if (hp <= 0)
            Die();

        if (isCharacter)
        {
            hp += LifeRegen * Time.deltaTime;
            hud?.SetSlider(hp);
        }   
        
        hp = Mathf.Clamp(hp, 0f, 100f);

        if (life)
        {
            Vector3 scale = life.transform.localScale;
            scale.x = hp / 1000f;
            life.transform.localScale = scale;
        }
    }

    public void LoseLife(float damage)
    {
        hp -= damage;
        
        if (LifeRegen > 0)
            hud?.SetSlider(hp);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        DestroyedEvent?.Invoke(gameObject);
    }
}