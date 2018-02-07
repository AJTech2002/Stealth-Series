using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrregularElipseConstraint : MonoBehaviour {

    #region Variables

    [Header("Outline Vars")]
    [Range(0, 1)]
    public float blendPower;
    public AnimationCurve bendCurve;
    public float smoothening;
    [Range(0,360)]
    public float ang2;
    public Transform testObject;

    [Header("ELIPSE TEST")]
    public float width;
    public float height;
    

    [Header("TEST VARS")]
    [Range(0,1)]
    public float perc;

    [Header("Cone Start")]
    public float offsetValue;
    public Vector3 dir;

    [Header("UP")]
    public Vector3 upPoint = new Vector3();

    [Header("DOWN")]
    public Vector3 downPoint = new Vector3();

    [Header("RIGHT")]
    public Vector3 rightPoint = new Vector3();

    [Header("LEFT")]
    public Vector3 leftPoint = new Vector3();

    //PRIVATES
    private Vector3 lastPoint = Vector3.zero;

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        DrawOffsets();
        DrawPoints();
        DrawOutlineCurrent();
        DrawCone();
     //   GetAB();
    }

    private void DrawOffsets ()
    {
        Vector3 locDir = transform.TransformDirection(dir.normalized) * offsetValue;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + locDir);
    }

    private void DrawPoints() {

        Gizmos.color = Color.red;

        Vector3 locDir = transform.TransformDirection(dir.normalized) * offsetValue;

        Gizmos.DrawWireSphere(Vector3.Lerp(l(upPoint) + locDir, transform.position, 1-perc), 0.03f);
        Gizmos.DrawWireSphere(Vector3.Lerp(l(downPoint) + locDir, transform.position, 1-perc), 0.03f);
        Gizmos.DrawWireSphere(Vector3.Lerp(l(leftPoint) + locDir, transform.position, 1-perc),  0.03f);
        Gizmos.DrawWireSphere(Vector3.Lerp(l(rightPoint) + locDir, transform.position, 1-perc), 0.03f);


    }

    private void DrawCone() {

        #region Variables
        Vector3 locDir = transform.TransformDirection(dir.normalized) * offsetValue;
        
        Gizmos.DrawLine(l(upPoint) + locDir, transform.position);
        Gizmos.DrawLine(l(downPoint) + locDir, transform.position);
        Gizmos.DrawLine(l(rightPoint) + locDir, transform.position);
        Gizmos.DrawLine(l(leftPoint) + locDir, transform.position);

       // Gizmos.DrawLine(l(downPoint) + locDir, l(upPoint) + locDir);
       // Gizmos.DrawLine(l(rightPoint) + locDir, l(leftPoint) + locDir);

        #endregion



    }

    string yStr;

    private void DrawOutlineCurrent()
    {
        #region Vars

       

        Vector3 up = Vector3.Lerp(l(upPoint) , transform.position, 1 - perc);
        Vector3 down = Vector3.Lerp(l(downPoint)  ,transform.position, 1 - perc);
        Vector3 right = Vector3.Lerp(l(leftPoint) , transform.position, 1 - perc);
        Vector3 left = Vector3.Lerp(l(rightPoint), transform.position, 1 - perc);
        Vector3 pos = testObject.position;

        #endregion
        //lastPoint = upPoint;
        //  for (int i = 0; i < 60; i++)
        // {
        #region Setup


         float currentAngle = Vector3.Angle(transform.up-transform.position,pos-transform.position);
        print(currentAngle);

            Vector3 a = Vector3.zero;
            Vector3 b = Vector3.zero;
            Vector3 c = Vector3.zero;
           // Vector3 pos = testObject.position;

            #endregion

           
            Vector3 lerp = Vector3.zero;

            lerp = transform.TransformPoint(new Vector3((0.5f * width) * Mathf.Cos(currentAngle) , (0.5f * height) * Mathf.Sin(currentAngle) , 0));
            lerp = Vector3.Lerp(lerp + transform.TransformDirection((dir.normalized * offsetValue)), transform.position, 1-perc);

       
            

            Gizmos.color = Color.yellow;

            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position+ transform.TransformDirection((dir.normalized * offsetValue)*perc), lerp );
           
            lastPoint = lerp;

            Gizmos.color = Color.red;


       // }

        lastPoint = Vector3.zero;


    }

    /*
     *  int rQuad = 0;
           if (currentAngle >= 0 && currentAngle <= 90)
            {
                a = up;
                b = right;
                c = down;
                rQuad = 1;
            }
           else if (currentAngle > 90 && currentAngle <= 180)
            {
                a = right;
                b = down;
                c = left;
                rQuad = 2;
            }
           else if (currentAngle > 180 && currentAngle <= 270)
            {
                a = down;
                b = left;
                c = up;
                rQuad = 3;
            }
           else if (currentAngle > 270 && currentAngle <= 360)
            {
                a = left;
                b = up;
                c = right;
                rQuad = 4;
            }


    */


    private void Update()
    {

       // Vector3 clampPoint = GetAB();

    }

    private Vector3 GetAB()
    {
        #region VARS
        Vector3 direct = transform.TransformDirection((dir.normalized * offsetValue)*perc);

        Vector3 up = l(upPoint);
        Vector3 down = l(downPoint);
        Vector3 right = l(leftPoint);
        Vector3 left = l(rightPoint);
        #endregion

        #region Setup
        Vector3 pos = testObject.position;
     
        float currentAngle = -Vector3.Angle(up-transform.position, pos-transform.position);

        print(currentAngle);

        #endregion


        Vector3 lerp = Vector3.zero;

        lerp = transform.TransformPoint(new Vector3((0.5f * width) * Mathf.Cos(currentAngle), (0.5f * height) * Mathf.Sin(currentAngle), 0));
        lerp = Vector3.Lerp(lerp + transform.TransformDirection((dir.normalized * offsetValue)), transform.position, 1 - perc);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(lerp, 0.03f);
       



        return Vector3.zero;
        
    }

    #endregion

    #region Helpers

    public Vector3 l (Vector3 lP)
    {
        return transform.TransformPoint(lP);
    }

    public Vector3 iL (Vector3 lP)
    {
        return transform.InverseTransformPoint(lP);
    }

    public Vector3 d(Vector3 a)
    {
        return a - transform.position;
    }

    public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }

    #endregion

}

