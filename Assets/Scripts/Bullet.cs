using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject ps;

    [SerializeField] private bool addPlayerVel;

    private Transform character;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Blaster").transform;
        rb.velocity = character.transform.forward * speed;

        if (addPlayerVel)
            rb.velocity += character.GetComponent<Rigidbody>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, character.position) > 100f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ps, transform.position, Quaternion.identity);
    }

    private void OnCollisionExit(Collision collision)
    {
        Destroy(gameObject);
    }
}