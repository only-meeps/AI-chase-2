using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    public bool isTouchingGround;
    public void OnCollisionStay(Collision collision)
    {
        isTouchingGround = true;
    }

    
    public void OnCollisionExit(Collision collision)
    {
        isTouchingGround = false;
    }
    
}
