using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject hand;
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected Animation anim;
    [SerializeField] protected StateMachine stateMachine;
    [SerializeField] protected Id stateId;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] private float bulletReloadTime;

    private const int ChamberSize = 6;

    public bool isReloading { get; private set; }
    public int chamber { get; protected set; }

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
    public abstract void DropGun();

    /// <summary>
    /// Try and select a gun from the floor and set as current weapon
    /// </summary>
    /// <param name="parent"></param>
    public abstract void GrabGun(Transform parent);

    /// <summary>
    /// Instantiate bullet
    /// </summary>
    public abstract void Shoot();
}