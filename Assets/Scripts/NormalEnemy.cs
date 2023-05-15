using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Transform target;
    private Rigidbody rb;
    private float damageTime = 2f;
    private float cooldown;
    private float rotateDis = 1f;

    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.Find("Character").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Plane plane = new Plane(transform.position, transform.right);


        if (Physics.Raycast(ray, out hit, rotateDis) && hit.transform.CompareTag("Walls"))
        {
            Draw.Ray(ray, Color.red);
            Vector3 point = hit.point + hit.normal;
            Draw.Point(point, .2f);

            transform.LookAt(point - hit.normal / 2f);
        }
        else
        {
            ray = new Ray(transform.position, transform.right);

            if (!(Physics.Raycast(ray, out hit, transform.right.magnitude * 2f, LayerMask.GetMask("Walls"))))
                transform.LookAt(target);
            else
                Draw.Ray(ray, Color.blue);

            rb.velocity = speed * transform.forward;
        }
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
        if (Time.time > cooldown && other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponent<Stats>().LoseLife(10f);
            cooldown = Time.time + damageTime;
        }
    }
}