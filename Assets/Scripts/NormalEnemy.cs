using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    
    private Transform target;
    private Rigidbody rb;
    
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
        rb.velocity = speed * transform.forward;
    }
}
