using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    Vector3 basketballTransform;

    BasketballLauncher launcher;

    int counter = 0;

    int bounceCount;

    // Start is called before the first frame update
    void Start()
    {
        launcher = FindObjectOfType<BasketballLauncher>();
        basketballTransform = transform.position;

        bounceCount = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Ground"))
        {
            counter++;

            if(counter > bounceCount)
            {
                //! TODO This is Spagetti Code. Please FIX THIS ASAP
                
                launcher.ballRb.useGravity = false;                 //access ball's rb and sets the gravity to 0
                launcher.ballRb.velocity = Vector3.zero;            //resets balls velocity on collision with ground
                transform.position = basketballTransform;           //resets balls position on collision with ground
                launcher.trajectoryLineRenderer.gameObject.SetActive(true);
                launcher.drawTrajectory = true;
                bounceCount = Random.Range(0, 2);                   //randomizes bounce count
                counter = 0;    //resets counter
            }

        }
    }
}