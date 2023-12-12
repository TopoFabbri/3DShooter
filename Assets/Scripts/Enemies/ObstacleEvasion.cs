using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Class to detect and evade obstacles
    /// </summary>
    public class ObstacleEvasion : MonoBehaviour
    {
        [SerializeField] private float radius = .5f;
        [SerializeField] private float detectionDis = 1f;

        /// <summary>
        /// Update character rotation to evade obstacles
        /// </summary>
        public void CheckAndEvade()
        {
            var trans = transform;
            var position = trans.position;
            var right = trans.right;
            var leftRayOrigin = position - right * radius;
            var rightRayOrigin = position + right * radius;

            var ray = new Ray(transform.position, trans.forward);
            Vector3 dir;

            Draw.Line(ray.origin, ray.direction, detectionDis);
            if (Physics.Raycast(ray, out var hit, detectionDis, LayerMask.GetMask("Walls")))
            {
                dir = Vector3.Cross(Vector3.up, hit.normal);
                transform.Rotate(Vector3.down, Vector3.Angle(dir, transform.forward));
                Draw.Line(transform.position, dir, 1f);
                return;
            }
            
            ray.origin = leftRayOrigin;
            
            Draw.Line(ray.origin, ray.direction, detectionDis);
            if (Physics.Raycast(ray, out hit, detectionDis, LayerMask.GetMask("Walls")))
            {
                dir = Vector3.Cross(Vector3.up, hit.normal);
                transform.Rotate(Vector3.down, Vector3.Angle(dir, transform.forward));
                Draw.Line(transform.position, dir, 1f);
                return;
            }

            ray.origin = rightRayOrigin;

            Draw.Line(ray.origin, ray.direction, detectionDis);
            
            if (!Physics.Raycast(ray, out hit, detectionDis, LayerMask.GetMask("Walls"))) return;
            dir = Vector3.Cross(Vector3.up, hit.normal);
            transform.Rotate(Vector3.down, Vector3.Angle(dir, transform.forward));
            Draw.Line(transform.position, dir, 1f);
        }
    }
}