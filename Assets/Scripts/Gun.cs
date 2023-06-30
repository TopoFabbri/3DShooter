using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject hand;
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected Animation anim;
    [SerializeField] protected StateMachine stateMachine;
    [SerializeField] protected Id stateId;

    protected Rigidbody Rb;
    protected BoxCollider Collider;
    protected int Chamber;

    private const int ChamberSize = 6;
    private float bulletReloadTime;

    public bool isReloading { get; private set; }
    public int chamber { get; }

    /// <summary>
    /// Gameplay-only update
    /// </summary>
    protected void OnUpdate()
    {
        if (Chamber <= 0)
            Reload();
    }

    /// <summary>
    /// Start action reload
    /// </summary>
    public void Reload()
    {
        isReloading = true;
        StartCoroutine(StopReloadOnTime(bulletReloadTime));
        
        Chamber = ChamberSize;
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