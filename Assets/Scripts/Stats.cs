using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float hp = 100f;
    [SerializeField] private float lifeRegen = 0f;
    [SerializeField] private Transform life;

    private void Start()
    {
    }

    private void Update()
    {
        if (hp <= 0)
            Die();

        hp += lifeRegen * Time.deltaTime;
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
    }

    private void Die()
    {
        if (!CompareTag("Character"))
            Destroy(gameObject);
    }

    public float GetHp()
    {
        return hp;
    }
}