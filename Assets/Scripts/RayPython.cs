using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class RayPython : Gun
{
    [SerializeField] private float strength = 10f;
    [SerializeField] private GameObject ps;
    [SerializeField] private float damage = 20;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    public override void DropGun()
    {
        rb.useGravity = true;
        collider.isTrigger = false;
        hand.SetActive(false);
        transform.parent = null;
        rb.AddForce(transform.forward * 2 + transform.up, ForceMode.Impulse);
    }

    public override void GrabGun(Transform parent)
    {
        rb.useGravity = false;
        collider.isTrigger = true;
        hand.SetActive(true);
        transform.parent = parent;
    }

    public override void Shoot()
    {
        Ray ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit)) 
        {
            Instantiate(ps, hit.point, Quaternion.identity);

            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<Rigidbody>()?.AddForce(transform.forward * strength, ForceMode.Impulse);
                hit.transform.gameObject.GetComponent<Stats>().LoseLife(damage);
            }
        }
    }
}
