using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootIK : MonoBehaviour {

    public ArmIK arm1;
    public ArmIK arm2;

    public SphericalConstraint constraint;

    private void LateUpdate()
    {
        SolveIK();
    }

    private void SolveIK()
    {
        Vector3 arm1V = arm1.SolveBackward(arm1.endEffector.position);
        Vector3 arm2V = arm2.SolveBackward(arm2.endEffector.position);

        Vector3 centroid = (arm1V + arm2V) / 2;

        centroid = constraint.clampIfNeeded(centroid);

        arm1.SolveForward(centroid);
        arm2.SolveForward(centroid);

        arm1.SetTempPos();
        arm2.SetTempPos();

        transform.position = centroid;

    }

}
