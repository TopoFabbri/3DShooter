using System;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private GameObject barrelExplosion;

    public void Explode()
    {
        var trans = transform;
        
        Instantiate(barrelExplosion, trans.position, trans.rotation);
    }
}
