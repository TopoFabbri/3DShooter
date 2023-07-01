using UnityEngine;

public class InstancePython : Gun
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject sprite;
    [SerializeField] private Transform character;
    [SerializeField] private Vector3 dropForce;
    
    private void OnEnable()
    {
        stateMachine.Subscribe(stateId, OnUpdate);
    }

    private void OnDisable()
    {
        stateMachine.UnSubscribe(stateId, OnUpdate);
    }

    /// <summary>
    /// Gameplay-only update
    /// </summary>
    private void OnUpdate()
    {
        CheckReload();
        sprite.transform.position = transform.position + Vector3.up;
        sprite.transform.LookAt(character.position);
    }

    public override void DropGun()
    {
        rb.useGravity = true;
        sprite.SetActive(true);
        boxCollider.isTrigger = false;
        hand.SetActive(false);
        var trans = transform;
        trans.parent = null;
        rb.AddForce(trans.forward * dropForce.z + trans.up * dropForce.y, ForceMode.Impulse);
    }

    public override void GrabGun(Transform parent)
    {
        rb.useGravity = false;
        sprite.SetActive(false);
        boxCollider.isTrigger = true;
        hand.SetActive(true);
        transform.parent = parent;
    }

    public override void Shoot()
    {
        if (!isReloading)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            anim.Play();
            
            chamber--;
        }
    }
}