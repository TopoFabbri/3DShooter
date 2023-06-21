using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private ObstacleEvasion obstacleEvasion;
    private Transform target;
    private Rigidbody rb;
    //TODO: Fix - Add [SerializeFieldAttribute]
    private float damageTime = 2f;
    private float cooldown;

    // Start is called before the first frame update
    private void Start()
    {
        //TODO: Fix - Add [RequireComponentAttribute]
        obstacleEvasion = GetComponent<ObstacleEvasion>();
        //TODO: Fix - Hardcoded value
        target = GameObject.Find("Character").transform;
        //TODO: Fix - Add [RequireComponentAttribute]
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(target);

        obstacleEvasion.CheckAndEvade();
        rb.velocity = speed * transform.forward;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponent<Stats>().LoseLife(10f);
            cooldown = Time.time + damageTime;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        //TODO: Fix - Hardcoded value
        if (Time.time > cooldown && other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponent<Stats>().LoseLife(10f);
            cooldown = Time.time + damageTime;
        }
    }
}