using UnityEngine;

public class InstancePython : Gun
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform character;

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

    public override void Shoot()
    {
        if (isReloading) return;
        
        base.Shoot();
        
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}