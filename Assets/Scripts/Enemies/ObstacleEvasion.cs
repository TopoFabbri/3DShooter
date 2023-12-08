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
        [SerializeField] private string wallsTag = "Walls";

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

            var ray = new Ray(leftRayOrigin, trans.forward);
            Vector3 dir;

            if (Physics.Raycast(ray, out var hit, detectionDis) && hit.transform.CompareTag(wallsTag))
            {
                dir = Vector3.Cross(Vector3.up, hit.normal);
                transform.Rotate(Vector3.down, Vector3.Angle(dir, transform.forward));
                return;
            }

            ray.origin = rightRayOrigin;

            if (Physics.Raycast(ray, out hit, detectionDis) && hit.transform.CompareTag(wallsTag))
            {
                dir = Vector3.Cross(Vector3.up, hit.normal);
                transform.Rotate(Vector3.down, Vector3.Angle(dir, transform.forward));
                return;
            }

            ray.origin = transform.position;

            if (!Physics.Raycast(ray, out hit, detectionDis) || !hit.transform.CompareTag(wallsTag)) return;

            dir = Vector3.Cross(Vector3.up, hit.normal);
            transform.Rotate(Vector3.down, Vector3.Angle(dir, transform.forward));
        }
    }
}