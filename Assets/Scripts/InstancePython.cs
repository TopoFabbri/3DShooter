using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InstancePython : Gun
{
    [SerializeField] private GameObject bulletPrefab;

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
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
