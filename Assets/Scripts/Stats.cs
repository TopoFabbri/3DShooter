using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private Transform life;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (hp <= 0)
            Die();

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
        Destroy(gameObject);
    }
}
