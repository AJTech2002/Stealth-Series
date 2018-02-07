using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootIK : MonoBehaviour
{

    public Transform parent;
    public Transform pelvis;

    public ArmIK arm1;
    public ArmIK arm2;

    public SphericalConstraint constraint;

    private Vector3 centroidOffset;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pelvis.position, transform.position);
    }

    private void LateUpdate()
    {

        SolveIK();

    }

    private void SolveIK()
    {

        arm1.SetupRefs();
        arm2.SetupRefs();

        Vector3 arm1V = arm1.SolveBackward(arm1.endEffector.position);
        Vector3 arm2V = arm2.SolveBackward(arm2.endEffector.position);

        Vector3 centroid = ((arm1V + arm2V + transform.position) / 3);

        if (!constraint.PointIsInSphere(centroid))
        {
            centroid = constraint.closestPointOnBounds(centroid);
        }

        arm2.SolveForward(centroid);
        arm1.SolveForward(centroid);


        arm1.UpdateAll();
        arm2.UpdateAll();

        transform.position = centroid;

     //   arm2.SetTempPos();
     //   arm1.SetTempPos();
    }



}
