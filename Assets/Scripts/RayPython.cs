using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPython : Gun
{
    [SerializeField] private float strength = 10f;
    [SerializeField] private GameObject ps;
    [SerializeField] private GameObject sprite;
    [SerializeField] private Transform character;
    [SerializeField] private float damage = 50f;

    private void Start()
    {
        //TODO: Fix - Add [RequireComponentAttribute]
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        base.Update();
        sprite.transform.position = transform.position + Vector3.up;
        sprite.transform.LookAt(character.position);
    }
    
    public override void DropGun()
    {
        //TODO: Fix - Repeated code
        rb.useGravity = true;
        sprite.SetActive(true);
        collider.isTrigger = false;
        hand.SetActive(false);
        transform.parent = null;
        rb.AddForce(transform.forward * 2 + transform.up, ForceMode.Impulse);
    }

    public override void GrabGun(Transform parent)
    {
        //TODO: Fix - Repeated code
        rb.useGravity = false;
        sprite.SetActive(false);
        collider.isTrigger = true;
        hand.SetActive(true);
        transform.parent = parent;
    }

    public override void Shoot()
    {
        //TODO: Fix - OOP
        if (!isReloading)
        {
            Ray ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(ps, hit.point, Quaternion.identity);

                if (hit.transform.gameObject.GetComponent<Stats>())
                    hit.transform.gameObject.GetComponent<Stats>().LoseLife(damage);

                if (hit.transform.gameObject.GetComponent<Rigidbody>())
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce((hit.transform.position - hit.point).normalized * strength, ForceMode.Impulse);
            }
            
            //TODO: Fix - OOP
            anim.Play();

            //TODO: Fix - OOP
            chamber--;
        }
    }
}