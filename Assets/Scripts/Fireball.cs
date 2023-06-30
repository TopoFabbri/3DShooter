using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Stats>(out var stats))
            stats.LoseLife(damage);
        
        Destroy(gameObject);
    }
}