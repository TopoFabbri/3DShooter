using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 25f;
    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO: Fix - Speed isn't being modified anywhere, it makes no sense to be updating it every frame
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        //TODO: Fix - TryGetComponent
        if (other.gameObject.GetComponent<Stats>())
            other.gameObject.GetComponent<Stats>().LoseLife(damage);

        Destroy(gameObject);
    }
}