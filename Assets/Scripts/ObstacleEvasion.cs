using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEvasion : MonoBehaviour
{
    [SerializeField]private float radius = .5f;
    [SerializeField]private float detectionDis = 1f;

    public void CheckAndEvade()
    {
        Vector3 leftRayOrigin = transform.position - transform.right * radius;
        Vector3 rightRayOrigin = transform.position + transform.right * radius;
        
        Ray ray = new Ray(leftRayOrigin, transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, detectionDis) && hit.transform.CompareTag("Walls"))
        {
            Vector3 dir = Vector3.Cross(Vector3.up, hit.normal);
            transform.Rotate(Vector3.down, Vector3.Angle(dir, transform.forward));
        }
        else
        {
            ray.origin = rightRayOrigin;

            if (Physics.Raycast(ray, out hit, detectionDis) && hit.transform.CompareTag("Walls"))
            {
                Vector3 dir = Vector3.Cross(Vector3.up, hit.normal);
                transform.Rotate(Vector3.down, Vector3.Angle(dir, transform.forward));
            }
            else
            {
                ray.origin = transform.position;

                if (Physics.Raycast(ray, out hit, detectionDis) && hit.transform.CompareTag("Walls"))
                {
                    Vector3 dir = Vector3.Cross(Vector3.up, hit.normal);
                    transform.Rotate(Vector3.down, Vector3.Angle(dir, transform.forward));
                }
            }
        }
    }
}
