﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmIK : MonoBehaviour
{

    #region Variables

    [Header("Options")]
    public Transform capsule;
    public bool drawJoints;
    public bool drawConstraints;
    public float currentAngle;
    [Range(0,2)]
    public float elbowPower;

    [Header("Joint References")]
    public Transform endEffector;
    public Transform centroidRef;
    public Transform elbowRef;
    public Transform upperRef;
    public Transform lowerRef;
    public Transform handRef;

    [Header("Mesh References")]
    public Transform upperMesh;
    public Transform lowerMesh;
    public Transform handMesh;

    public Vector3 upperOffset;
    public Vector3 lowerOffset;

    [Header("Constraints")]
    public SphericalConstraint upperConstraint;
    public IrregularElipseConstraint lowerConstraint;


    [Header("Angular Constraints")]
    public Vector3 minAngularUpper;
    public Vector3 maxAngularUpper;


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
        if (drawJoints)
             JointDrawing();
    }
  
    private void JointDrawing()
    {
        if (handRef != null)
        {

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(upperRef.position, centroidRef.position);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(upperRef.position, lowerRef.position);
            Gizmos.DrawLine(lowerRef.position, handRef.position);

            Gizmos.color = Color.cyan;
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


    public void SetupRefs()
    {
        if (lowerMesh != null)
        {
            upperRef.position = upperMesh.position;
            lowerRef.position = lowerMesh.position;
            handRef.position = handMesh.position;
        }
    }

    public Vector3 SolveBackward(Vector3 endEffector)
    {
        tempCentroid = centroidPos;

        tempHand = endEffector;

        Vector3 handToLower = (lowerRef.position - tempHand).normalized * lowerToHandDist;
        tempLower = (tempHand + handToLower + (elbowRef.position - lowerRef.position).normalized * elbowPower);

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

    public void UpdateAll()
    {
        tempUpper = upperConstraint.clampIfNeeded(tempUpper);
        tempLower = (tempUpper + (tempLower - tempUpper).normalized * upperToLowerDist);
        tempHand = tempLower + (tempHand - tempLower).normalized * lowerToHandDist;

        

        upperRef.position = tempUpper;
        lowerRef.position = tempLower;
        handRef.position = tempHand;

        
    }

   


    #endregion

}

