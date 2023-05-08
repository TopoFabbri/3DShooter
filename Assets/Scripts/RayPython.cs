using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPython : Gun
{

    private GameObject hand;
    private Rigidbody rb;
    private BoxCollider collider;

    private void Start()
    {
        hand = GameObject.Find("Hand");
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    public override void DropGun()
    {
        rb.useGravity = true;
        collider.isTrigger = false;
        hand.SetActive(false);
        transform.parent = null;
    }

    public override void GrabGun(Transform parent)
    {
        rb.useGravity = false;
        collider.isTrigger = true;
        hand.SetActive(true);
        transform.parent = parent;
    }

    public override void Shoot(Transform bulletSpawnPoint)
    {
        Ray ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit)) 
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {

            }
        }
    }
}
