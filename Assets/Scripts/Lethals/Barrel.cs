using UnityEngine;

namespace Lethals
{
    /// <summary>
    /// Barrel lethal controller
    /// </summary>
    public class Barrel : Lethal
    {
        [SerializeField] private Stats.Stats stats;
    
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
}