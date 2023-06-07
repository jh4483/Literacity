using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    [Header("References")]
    public Rigidbody ballRb;
    public Transform[] backBoard;
    public int targetIndex = 0;
    public bool drawTrajectory = false;

    [Header("Parabolic Path Parameters")]
    public float maxHeight = 15f;
    public float gravity = -18f;

    [Header("Target Keys")]
    public KeyCode target1;
    public KeyCode target2;
    public KeyCode target3;
    public KeyCode target4;

    void Start()
    {
        ballRb.useGravity = false;          //sets gravity to false so that gravity isn't enabled till ball is launched
        
        drawTrajectory = true;
    }

    void Update()
    {
        SelectTarget();                      //Selects the target to aim at

        DrawTrajectory();                    //Draws the trajectory of the ball

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();                           
        }
    }

    LaunchData CalculateBallLaunchVelocity()
    {
        float displacementY = backBoard[targetIndex].position.y - ballRb.position.y;                                                                                       //Vertical displacement calculation
        Vector3 displacementXZ = new Vector3(backBoard[targetIndex].position.x - ballRb.position.x, 0, backBoard[targetIndex].position.z - ballRb.position.z);            //Horizontal displacement calculation

        float timeOfFlight = Mathf.Sqrt(-2 * maxHeight / gravity) + Mathf.Sqrt(2 * (displacementY - maxHeight) / gravity);                      //Time of flight calculation using eqns of motion              

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * maxHeight);                                                                  //Vertical velocity(initial) calculation using eqns of motion
        Vector3 velocityXZ = displacementXZ / timeOfFlight;                                                                                     //Horizontal velocity(initial) calculation using eqns of motion

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), timeOfFlight);                                                                                                          //Total initial velocity
    }

    void DrawTrajectory()
    {
        if(!drawTrajectory)
        {
            return;
        }
        
        if(drawTrajectory)
        {
            LaunchData launcData = CalculateBallLaunchVelocity();
            Vector3 previousDrawPoint = ballRb.position;

            int resolution = 30;
            for(int i = 1; i <= resolution; i++)
            {
                float simulationTime = i / (float)resolution * launcData.timeToTarget;
                Vector3 displacement = launcData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = ballRb.position + displacement;
                Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
                previousDrawPoint = drawPoint;
            }
        }
    }

    void Launch()
    {   
        Physics.gravity = Vector3.up * gravity;                                                                                                 //Sets gravity to the value of gravity variable
        ballRb.useGravity = true;
        drawTrajectory = false;                                                                                                               //Enables gravity  
        ballRb.velocity = CalculateBallLaunchVelocity().initialVelocity;                                                                                        //Sets the velocity of the ball to the value returned by CalculateBallLaunchVelocity()
    }
    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

    void SelectTarget()
    {
        if(Input.GetKeyDown(target1))
        {
            targetIndex = 0;
        }
        else if (Input.GetKeyDown(target2))
        {
            targetIndex = 1;
        }
        else if (Input.GetKeyDown(target3))
        {
            targetIndex = 2;
        }
        else if (Input.GetKeyDown(target4))
        {
            targetIndex = 3;
        }
    }

}