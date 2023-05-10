using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float hp = 100;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (hp <= 0)
            Die();
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
