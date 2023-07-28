using UnityEngine;

public class RayPython : Gun
{
    [SerializeField] private float strength = 10f;
    [SerializeField] private Transform character;
    [SerializeField] private float damage = 50f;

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
        
        var ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);

        base.Shoot();

        if (!Physics.Raycast(ray, out var hit)) return;
        
        weaponVFX.PlayHitExplosion(hit.point);

        if (hit.transform.gameObject.GetComponent<Stats>())
            hit.transform.gameObject.GetComponent<Stats>().LoseLife(damage);

        if (hit.transform.gameObject.GetComponent<Rigidbody>())
            hit.transform.gameObject.GetComponent<Rigidbody>().AddForce((hit.transform.position - hit.point).normalized * strength, ForceMode.Impulse);
    }
}