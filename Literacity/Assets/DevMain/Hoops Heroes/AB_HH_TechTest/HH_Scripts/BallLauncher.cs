using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Rigidbody ball;
    public Transform[] target;
    public int targetIndex;

    public float height = 25f;
    public float gravity = -18f;

    void Start()
    {
        targetIndex = 3;

        //ball = GetComponent<Rigidbody>();
        ball.useGravity = false;
    }

    void Update()
    {
        ResetBall();
        SetTarget();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;

        ball.velocity = CalculateLaunchVelocity();
    }

    Vector3 CalculateLaunchVelocity()
    {
        float displacementY = target[targetIndex].position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target[targetIndex].position.x - ball.position.x, 0, target[targetIndex].position.z - ball.position.z);

        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        return velocityXZ + velocityY; 
    }

    void SetTarget()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            targetIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            targetIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            targetIndex = 3;
        }
    }

    void ResetBall()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
            ball.useGravity = false;
            ball.transform.position = transform.position;
        }
    }
}
