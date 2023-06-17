using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//TODO: Documentation - Add summary
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 450f;
    [SerializeField] private Rigidbody rb;
    //TODO: Fix - Unclear name
    [SerializeField] private GameObject ps;
    [SerializeField] private float damage = 50f;
    [FormerlySerializedAs("life")] [SerializeField] private float lifeTime = 5f;

    [SerializeField] private bool addPlayerVel;

    private float destroyTime;

    private Transform character;

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    // Start is called before the first frame update
    void Start()
    {
        //TODO: Fix - Hardcoded value
        character = GameObject.Find("Character").GetComponentInChildren<Gun>().transform;
        rb.velocity = transform.forward * speed;

        if (addPlayerVel)
            rb.velocity += character.GetComponent<Rigidbody>().velocity;

        destroyTime = Time.time + lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Fix - Could be coroutine or timed invoke
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