using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public abstract void DropGun();

    public abstract void GrabGun(Transform parent);

    public abstract void Shoot(Transform bulletSpawnPoint);
}