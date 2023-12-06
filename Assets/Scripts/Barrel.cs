using UnityEngine;

public class Barrel : Lethal
{
    [SerializeField] private Stats stats;
    
    private void OnEnable()
    {
        stats.OnDie += Explode;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        stats.OnDie -= Explode;
    }
}