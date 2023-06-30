using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InstancePython : Gun
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject sprite;
    [SerializeField] private Transform character;

    private void Start()
    {
        //TODO: Fix - Add [RequireComponentAttribute]
        Rb = GetComponent<Rigidbody>();
        Collider = GetComponent<BoxCollider>();
    }

    private void OnUpdate()
    {
        base.OnUpdate();
        sprite.transform.position = transform.position + Vector3.up;
        sprite.transform.LookAt(character.position);
    }

    public override void DropGun()
    {
        Rb.useGravity = true;
        sprite.SetActive(true);
        Collider.isTrigger = false;
        hand.SetActive(false);
        transform.parent = null;
        //TODO: Fix - Hardcoded value
        Rb.AddForce(transform.forward * 2 + transform.up, ForceMode.Impulse);
    }

    public override void GrabGun(Transform parent)
    {
        Rb.useGravity = false;
        sprite.SetActive(false);
        Collider.isTrigger = true;
        hand.SetActive(true);
        transform.parent = parent;
    }

    public override void Shoot()
    {
        if (!isReloading)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            anim.Play();
            
            Chamber--;
        }
    }
}