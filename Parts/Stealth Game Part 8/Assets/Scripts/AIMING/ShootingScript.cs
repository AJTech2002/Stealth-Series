using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

    public Animator aimController;

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            aimController.SetInteger("Firing", 1);
        }
        else
        {
            aimController.SetInteger("Firing", 0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            aimController.SetInteger("State", 1);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            aimController.SetInteger("State", 0);
        }


    }


}
