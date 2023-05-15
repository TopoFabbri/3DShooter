using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 450f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject ps;
    [SerializeField] private float damage = 20f;
    [FormerlySerializedAs("life")] [SerializeField] private float lifeTime = 5f;

    [SerializeField] private bool addPlayerVel;

    private float destroyTime;

    private Transform character;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character").GetComponentInChildren<Gun>().transform;
        rb.velocity = transform.forward * speed;

        if (addPlayerVel)
            rb.velocity += character.GetComponent<Rigidbody>().velocity;

        destroyTime = Time.time + lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > destroyTime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        
        if (collision.gameObject.GetComponent<Stats>())
            collision.gameObject.GetComponent<Stats>().LoseLife(damage);
    }
}