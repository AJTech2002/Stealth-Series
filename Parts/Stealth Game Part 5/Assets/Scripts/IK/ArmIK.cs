using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmIK : MonoBehaviour
{

    #region Variables

    [Header("Options")]
    [Range(0,2)]
    public float elbowPower;

    [Header("Joint References")]
    public Transform endEffector;
    public Transform centroidRef;
    public Transform elbowRef;
    public Transform upperRef;
    public Transform lowerRef;
    public Transform handRef;


    [HideInInspector]
    public Vector3 centroidPos;

    private Vector3 tempCentroid;
    private Vector3 tempUpper;
    private Vector3 tempLower;
    private Vector3 tempHand;

    private float centroidToUpperDist;
    private float upperToLowerDist;
    private float lowerToHandDist;

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        
        if (handRef != null)
        {

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(upperRef.position, centroidRef.position);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(upperRef.position, lowerRef.position);
            Gizmos.DrawLine(lowerRef.position, handRef.position);

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(lowerRef.position, elbowRef.position);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(endEffector.position, 0.15f);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(handRef.position, endEffector.position);


        }


    }


    #endregion

    #region Setup 

    private void Awake()
    {
        SetupVariables();
    }

    private void SetupVariables()
    {
        centroidToUpperDist = Vector3.Distance(upperRef.position, centroidRef.position);
        upperToLowerDist = Vector3.Distance(upperRef.position, lowerRef.position);
        lowerToHandDist = Vector3.Distance(lowerRef.position, handRef.position);

        centroidPos = centroidRef.position;
        tempCentroid = centroidPos;

    }

    #endregion

    #region Algorithm

    private void LateUpdate()
    {
        SolveBackward(endEffector.position);
        SolveForward(centroidPos);
        SetTempPos();
    }

    public Vector3 SolveBackward(Vector3 endEffector)
    {
        tempCentroid = centroidPos;

        tempHand = endEffector;

        Vector3 handToLower = (lowerRef.position - tempHand).normalized * lowerToHandDist;
        tempLower = tempHand + handToLower + (elbowRef.position - lowerRef.position).normalized * elbowPower;

        Vector3 lowerToUpper = (upperRef.position - tempLower).normalized * upperToLowerDist;
        tempUpper = tempLower + lowerToUpper;

        Vector3 upperToCentroid = (centroidRef.position - tempUpper).normalized * centroidToUpperDist;
        tempCentroid = tempUpper + upperToCentroid;


        return tempCentroid;
    }

    public void SolveForward(Vector3 rootPoint)
    {

        tempCentroid = rootPoint;

        Vector3 centroidToUpper = (tempUpper - tempCentroid).normalized * centroidToUpperDist;
        tempUpper = tempCentroid + centroidToUpper;

        Vector3 upperToLower = (tempLower - tempUpper).normalized * upperToLowerDist;
        tempLower = tempUpper + upperToLower;

        Vector3 lowerToHand = (tempHand - tempLower).normalized * lowerToHandDist;
        tempHand = tempLower + lowerToHand;

    }

    public void SetTempPos()
    {

        upperRef.position = tempUpper;
        lowerRef.position = tempLower;
        handRef.position = tempHand;

        centroidPos = centroidRef.position;

    }



    #endregion

}
