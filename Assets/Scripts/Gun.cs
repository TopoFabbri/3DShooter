using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject hand;
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected Animation anim;

    protected Rigidbody rb;
    protected BoxCollider collider;
    protected bool isReloading = false;
    protected int chamber = 0;

    private int chamberSize = 6;
    private float bulletReloadTime = 0f;
    private float reloadCooldown = 1f;

    protected void Update()
    {
        if (chamber <= 0)
            Reload();

        //TODO: Fix - Could be a coroutine
        if (Time.time > bulletReloadTime)
            isReloading = false;
    }

    //TODO: Fix - Could be a coroutine
    public void Reload()
    {
        if (!isReloading)
        {
            isReloading = true;
            bulletReloadTime = Time.time + reloadCooldown * (chamberSize - chamber);
            chamber = chamberSize;
        }
    }

    public abstract void DropGun();

    public abstract void GrabGun(Transform parent);

    public abstract void Shoot();

    //TODO: Fix - Should be native Setter/Getter
    public int GetBullets()
    {
        return chamber;
    }

    //TODO: Fix - Should be native Setter/Getter
    public bool GetReloading()
    {
        return isReloading;
    }
}