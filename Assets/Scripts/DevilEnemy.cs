using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float fireDis = 5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private GameObject fireBall;

    private Transform target;
    private Rigidbody rb;
    private List<GameObject> fireBalls = new List<GameObject>();
    private List<float> fbTime = new List<float>();
    private float nextFireTime = 0;

    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.Find("Character").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(target);
        
        if (Time.time > nextFireTime || nextFireTime < cooldown)
        {
            if (Vector3.Distance(transform.position, target.position) > fireDis || fireBalls.Count >= 3)
                rb.velocity = speed * transform.forward;
            else
                Shoot();
        }
    }

    private void Shoot()
    {
        nextFireTime = Time.time + cooldown;
        fireBalls.Add(Instantiate<GameObject>(fireBall, transform.position + transform.forward, transform.rotation));
    }
}