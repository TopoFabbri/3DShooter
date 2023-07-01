using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject hand;
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected StateMachine stateMachine;
    [SerializeField] protected Id stateId;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] protected GameObject sprite;
    [SerializeField] protected WeaponVFX weaponVFX;

    [SerializeField] private float bulletReloadTime;
    [SerializeField] private Vector3 dropForce;
    
    private const int ChamberSize = 6;

    public bool isReloading { get; private set; }
    public int chamber { get; private set; }

    /// <summary>
    /// Gameplay-only update
    /// </summary>
    protected void CheckReload()
    {
        if (chamber <= 0)
            Reload();
    }

    /// <summary>
    /// Start action reload
    /// </summary>
    public void Reload()
    {
        isReloading = true;
        StartCoroutine(StopReloadOnTime(bulletReloadTime * (ChamberSize - chamber)));
        
        chamber = ChamberSize;
    }
        
    /// <summary>
    /// Wait for 'time' then stop reload action
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator StopReloadOnTime(float time)
    {
        yield return new WaitForSeconds(time);
        isReloading = false;
    }
    
    /// <summary>
    /// Release grabbed weapon
    /// </summary>

    public void DropGun()
    {
        var trans = transform;
        
        rb.useGravity = true;
        sprite.SetActive(true);
        boxCollider.isTrigger = false;
        hand.SetActive(false);
        trans.parent = null;
        rb.AddForce(trans.forward * dropForce.z + trans.up * dropForce.y, ForceMode.Impulse);
    }

    /// <summary>
    /// Try and select a gun from the floor and set as current weapon
    /// </summary>
    /// <param name="parent"></param>

    public void GrabGun(Transform parent)
    {
        rb.useGravity = false;
        sprite.SetActive(false);
        boxCollider.isTrigger = true;
        hand.SetActive(true);
        transform.parent = parent;
    }

    /// <summary>
    /// Call action shoot
    /// </summary>
    public virtual void Shoot()
    {
        chamber--;
        
        weaponVFX.Shoot();
    }
}