using System.Collections;
using UnityEngine;

namespace Lethals
{
    /// <summary>
    /// Bomb lethal controller
    /// </summary>
    public class Bomb : Lethal
    {
        [SerializeField] private float timeToExplode = 5f;
    
        public float ThrowForce { private get; set; } = 7f;
            
        private void OnEnable()
        {
            StartCoroutine(ExplodeOnTime(timeToExplode));
        
            rb.AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
        }
    
        /// <summary>
        /// Wait for given time and explode
        /// </summary>
        /// <param name="time">Time before explosion</param>
        /// <returns></returns>
        private IEnumerator ExplodeOnTime(float time)
        {
            yield return new WaitForSeconds(time);
            Explode();
        }
    }
}
