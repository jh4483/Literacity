using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher_HH_Script : MonoBehaviour
{
    [SerializeField] private Rigidbody ball;
    [SerializeField] private Transform[] target;
    [SerializeField] private int targetIndex;
    [SerializeField] private GameObject ballSprite;

    [SerializeField] private float height = 25f;
    [SerializeField] private float gravity = -18f;
    [SerializeField] private bool isLaunched = false;
    // Start is called before the first frame update
    void Start()
    {
        targetIndex = 0;

        ball.useGravity = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetBall();
        }
    }

    public void Launch()
    {
        isLaunched = true;

        ball.GetComponent<TargetFollow_HH_Scripts>().enabled = false;

        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;

        ball.velocity = CalculateLaunchVelocity();
    }

    public Vector3 CalculateLaunchVelocity()
    {
        float displacementY = target[targetIndex].position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target[targetIndex].position.x - ball.position.x, 0, target[targetIndex].position.z - ball.position.z);

        float time = Mathf.Sqrt(-2 * height/gravity) + Mathf.Sqrt(2 *(displacementY - height)/gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        return velocityXZ + velocityY;
    }

    public void ResetBall()
    {
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        ball.useGravity = false;
        ball.GetComponent<TargetFollow_HH_Scripts>().enabled = true;

        isLaunched = false;
    }
}
