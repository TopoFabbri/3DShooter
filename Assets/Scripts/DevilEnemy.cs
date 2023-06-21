using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float fireDis = 5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float longCooldown = 5f;
    [SerializeField] private GameObject fireBall;
    //TODO: TP2 - Remove unused methods/variables/classes
    [SerializeField] private Transform life;

    private ObstacleEvasion obstacleEvasion;
    private Transform target;
    private Rigidbody rb;
    //TODO: TP2 - Remove unused methods/variables/classes
    private List<float> fbTime = new List<float>();
    private float nextFireTime = 0;
    private int fireCount = 0;

    // Start is called before the first frame update
    private void Start()
    {
        //TODO: Fix - Add [RequireComponentAttribute]
        obstacleEvasion = GetComponent<ObstacleEvasion>();
        target = GameObject.Find("Character").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(target);

        //TODO: Fix - Could be a coroutine
        if (Time.time > nextFireTime)
        {
            if (Vector3.Distance(transform.position, target.position) > fireDis)
            {
                obstacleEvasion.CheckAndEvade();
                rb.velocity = speed * transform.forward;
            }
            else
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        fireCount++;

        nextFireTime = Time.time + (fireCount >= 3 ? longCooldown : cooldown);

        if (fireCount >= 3)
            fireCount = 0;

        Instantiate<GameObject>(fireBall, transform.position + transform.forward, transform.rotation);
    }
}