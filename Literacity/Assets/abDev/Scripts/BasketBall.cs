using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    [Header("References")]
    public Rigidbody ballRb;
    public Transform backBoard;

    [Header("Parabolic Path Parameters")]
    public float maxHeight = 15f;
    public float gravity = -18f;

    void Start()
    {
        ballRb.useGravity = false;          //sets gravity to false so that gravity isn't enabled till ball is launched
    }

    Vector3 CalculateBallLaunchVelocity()
    {
        float displacementY = backBoard.position.y - ballRb.position.y;                                                                         //Vertical displacement calculation
        Vector3 displacementXZ = new Vector3(backBoard.position.x - ballRb.position.x, 0, backBoard.position.z - ballRb.position.z);            //Horizontal displacement calculation

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * maxHeight);                                                                  //Vertical velocity(initial) calculation using eqns of motion
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * maxHeight / gravity) + Mathf.Sqrt(2 * (displacementY - maxHeight) / gravity));   //Horizontal velocity(initial) calculation using eqns of motion

        return velocityXZ + velocityY;                                                                                                          //Total initial velocity
    }

    void Launch()
    {   
        Physics.gravity = Vector3.up * gravity;                                                                                                 //Sets gravity to the value of gravity variable
        ballRb.useGravity = true;                                                                                                               //Enables gravity  
        ballRb.velocity = CalculateBallLaunchVelocity();                                                                                        //Sets the velocity of the ball to the value returned by CalculateBallLaunchVelocity()
    }


}
