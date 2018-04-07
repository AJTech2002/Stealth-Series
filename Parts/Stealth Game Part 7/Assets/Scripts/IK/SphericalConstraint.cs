using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalConstraint : MonoBehaviour {

    public float sphereRadius;
    public Transform relativeObject;
    public Vector3 offset;

    Vector3 centerPoint;

    private void Update()
    {
        centerPoint = relativeObject.TransformPoint(offset);
    }

    private void OnDrawGizmos()
    {
        Vector3 c = relativeObject.TransformPoint(offset);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(c, sphereRadius);
    }

    public bool PointIsInSphere (Vector3 point)
    {
        Vector3 originalPoint = centerPoint;
        float radius = sphereRadius;

        Vector3 vecDist = originalPoint - point;
        float fDistSq = Vector3.Dot(vecDist, vecDist);

        if (fDistSq < (radius*radius))
        {
            return true;
        }

        return false;

    }

    public Vector3 closestPointOnBounds(Vector3 point)
    {
        Vector3 originalCenter = centerPoint;
        float radius = sphereRadius;

        Vector3 dif = point - originalCenter;
        Vector3 returnPoint = centerPoint + (radius / dif.magnitude) * dif * 1f;

        return returnPoint;

    }


    public Vector3 clampIfNeeded (Vector3 point)
    {
        if (PointIsInSphere(point))
            return point;
        else
            return closestPointOnBounds(point);
    }


}
