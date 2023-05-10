using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject hand;
    [SerializeField] protected Transform bulletSpawnPoint;
    protected Rigidbody rb;
    protected BoxCollider collider;

    public abstract void DropGun();

    public abstract void GrabGun(Transform parent);

    public abstract void Shoot();
}