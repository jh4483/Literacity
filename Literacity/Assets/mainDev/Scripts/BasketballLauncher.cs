using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballLauncher : MonoBehaviour
{
    [Header("References")]
    public Rigidbody ballRb;
    public Transform[] backBoard;
    public Transform[] missed;
    public int targetIndex = 0;
    public bool drawTrajectory = false;
    public LayerMask layer;
    Ray ray;
    RaycastHit hit;
    string targetName;

    [Header("Parabolic Path Parameters")]
    public float maxHeight = 5f;
    public float gravity = -18f;

    public static int saveTargetIndex;
    public static GameObject hitBackboard;
    public LineRenderer trajectoryLineRenderer;


    void Start()
    {
        ballRb.useGravity = false;    
        drawTrajectory = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                targetName = hit.transform.name;
                hitBackboard = hit.transform.gameObject;

                // Debug.Log(hit.transform.name);
            }
        }

        SelectTarget();                      //Selects the target to aim at

        DrawTrajectory();                    //Draws the trajectory of the ball
    }

    LaunchData CalculateBallLaunchVelocity()
    {
        float displacementY;
        Vector3 displacementXZ;
        float timeOfFlight;
        Vector3 velocityY;
        Vector3 velocityXZ;


        if(TargetCheck.targetChecked)
        {
            displacementY = backBoard[targetIndex].position.y - ballRb.position.y;                                                                                       //Vertical displacement calculation
            displacementXZ = new Vector3(backBoard[targetIndex].position.x - ballRb.position.x, 0, backBoard[targetIndex].position.z - ballRb.position.z);            //Horizontal displacement calculation

            timeOfFlight = Mathf.Sqrt(-2 * maxHeight / gravity) + Mathf.Sqrt(2 * (displacementY - maxHeight) / gravity);                      //Time of flight calculation using eqns of motion              

            velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * maxHeight);                                                                  //Vertical velocity(initial) calculation using eqns of motion
            velocityXZ = displacementXZ / timeOfFlight;                                                                                     //Horizontal velocity(initial) calculation using eqns of motion

            return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), timeOfFlight);                                               //Total initial velocity
        }

        //Same code as the one in the top but with a different arry to calculate the trajectory for the missed shots

        displacementY = missed[targetIndex].position.y - ballRb.position.y;
        displacementXZ = new Vector3(missed[targetIndex].position.x - ballRb.position.x, 0, missed[targetIndex].position.z - ballRb.position.z);

        timeOfFlight = Mathf.Sqrt(-2 * maxHeight / gravity) + Mathf.Sqrt(2 * (displacementY - maxHeight) / gravity);

        velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * maxHeight);
        velocityXZ = displacementXZ / timeOfFlight;
            
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), timeOfFlight);                                               //Total initial velocity

    }

   void DrawTrajectory()
    {
        if (!drawTrajectory)
        {
            return;
        }
        
        if (drawTrajectory)
        {
            LaunchData launchData = CalculateBallLaunchVelocity();
            Vector3 previousDrawPoint = ballRb.position;

            int resolution = 30;
            Vector3[] trajectoryPoints = new Vector3[resolution];

            for (int i = 1; i <= resolution; i++)
            {
                float simulationTime = i / (float)resolution * launchData.timeToTarget;
                Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = ballRb.position + displacement;

                trajectoryPoints[i - 1] = drawPoint;

                previousDrawPoint = drawPoint;
            }

            trajectoryLineRenderer.positionCount = resolution;
            trajectoryLineRenderer.SetPositions(trajectoryPoints);
        }
    }


    public void Launch()
    {   
        trajectoryLineRenderer.gameObject.SetActive(false);
        Physics.gravity = Vector3.up * gravity;                                                                                               //Sets gravity to the value of gravity variable
        ballRb.useGravity = true;
        drawTrajectory = false;                                                                                                          //Enables gravity  
        ballRb.velocity = CalculateBallLaunchVelocity().initialVelocity;                                                                                      //Sets the velocity of the ball to the value returned by CalculateBallLaunchVelocity()
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
        if(targetName == "Backboard (1)")
        {
            targetIndex = 0;
            saveTargetIndex = targetIndex;
        }
        else if(targetName == "Backboard (2)")
        {
            targetIndex = 1;
            saveTargetIndex = targetIndex;
        }
        else if(targetName == "Backboard (3)")
        {
            targetIndex = 2;
            saveTargetIndex = targetIndex;
        }
        else if(targetName == "Backboard (4)")
        {
            targetIndex = 3;
            saveTargetIndex = targetIndex;
        }
    }

}
