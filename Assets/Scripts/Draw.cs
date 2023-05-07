using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public static Color color = Color.white;
    private static float pointDestroyTime = 0f;
    private static Vector3 pointPos = new Vector3();

    public static void WireCube(Vector3 center, Vector3 size)
    {
        Vector3 halfSize = size / 2f;

        Vector3 topLeftFront = center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
        Vector3 topRightFront = center + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
        Vector3 bottomRightFront = center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
        Vector3 bottomLeftFront = center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
        Vector3 topLeftBack = center + new Vector3(-halfSize.x, halfSize.y, halfSize.z);
        Vector3 topRightBack = center + new Vector3(halfSize.x, halfSize.y, halfSize.z);
        Vector3 bottomRightBack = center + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
        Vector3 bottomLeftBack = center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);

        Debug.DrawLine(topLeftFront, topRightFront, Color.white);
        Debug.DrawLine(topRightFront, bottomRightFront, Color.white);
        Debug.DrawLine(bottomRightFront, bottomLeftFront, Color.white);
        Debug.DrawLine(bottomLeftFront, topLeftFront, Color.white);

        Debug.DrawLine(topLeftFront, topLeftBack, Color.white);
        Debug.DrawLine(topRightFront, topRightBack, Color.white);
        Debug.DrawLine(bottomRightFront, bottomRightBack, Color.white);
        Debug.DrawLine(bottomLeftFront, bottomLeftBack, Color.white);

        Debug.DrawLine(topLeftBack, topRightBack, Color.white);
        Debug.DrawLine(topRightBack, bottomRightBack, Color.white);
        Debug.DrawLine(bottomRightBack, bottomLeftBack, Color.white);
        Debug.DrawLine(bottomLeftBack, topLeftBack, Color.white);
    }

    public static void Point(Vector3 point, float size)
    {
        Debug.DrawLine(point - Vector3.up * size / 2f, point + Vector3.up * size / 2f, color);
        Debug.DrawLine(point - Vector3.left * size / 2f, point + Vector3.left * size / 2f, color);
        Debug.DrawLine(point - Vector3.forward * size / 2f, point + Vector3.forward * size / 2f, color);
    }
    
    public static void Point(Vector3 point, float size, float delay)
    {
        pointDestroyTime = Time.time + delay;
        pointPos = point;
        
        Debug.DrawLine(point - Vector3.up * size / 2f, point + Vector3.up * size / 2f, color);
        Debug.DrawLine(point - Vector3.left * size / 2f, point + Vector3.left * size / 2f, color);
        Debug.DrawLine(point - Vector3.forward * size / 2f, point + Vector3.forward * size / 2f, color);
    }

    private void Update()
    {
        if (Time.time <= pointDestroyTime)
            Point(pointPos, .5f);
    }
}