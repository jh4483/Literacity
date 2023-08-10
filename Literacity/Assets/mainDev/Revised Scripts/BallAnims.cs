using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnims : MonoBehaviour
{
    public GameObject ballSprite;

    public void ShootBallL()
    {
        Debug.Log("Shoot Ball");
        ballSprite.SetActive(true);

        //if()
        // {
        //     SBLMade();
        // }
        // else if()
        // {
        //     SBLMissed();
        // }
    }

    void SBLMade()
    {
        if(!ballSprite.GetComponent<Animator>().GetBool("Ball_Shoot_L"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_L", true);
        }

        else if(ballSprite.GetComponent<Animator>().GetBool("Ball_Shoot_L"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_L", false);
        }
    }

    void SBLMissed()
    {
        if(!ballSprite.GetComponent<Animator>().GetBool("Ball_Shoot_Missed"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_Missed", true);
        }

        else if(ballSprite.GetComponent<Animator>().GetBool("Ball_Shoot_Missed"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_Missed", false);
        }
    }

    public void ShootBallDribbleL()
    {
        Debug.Log("DribbleNShoot");
        ballSprite.SetActive(true);

        if(!ballSprite.GetComponent<Animator>().GetBool("Ball_DribbleNShoot_L"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_DribbleNShoot_L", true);
        }

        else if(ballSprite.GetComponent<Animator>().GetBool("Ball_DribbleNShoot_L"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_DribbleNShoot_L", true);
        }

    }

    public void ShootBallDribbleR()
    {
        Debug.Log("DribbleNShoot_R");
        ballSprite.SetActive(true);

        if(!ballSprite.GetComponent<Animator>().GetBool("Ball_DribbleNShoot_L"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_DribbleNShoot_L", true);
        }

        else if(ballSprite.GetComponent<Animator>().GetBool("Ball_DribbleNShoot_L"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_DribbleNShoot_L", true);
        }
    }

    public void Fade()
    {
        Debug.Log("Fade");
        ballSprite.SetActive(true);

        if(!ballSprite.GetComponent<Animator>().GetBool("Ball_Fade"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_Fade", true);
        }

        else if(ballSprite.GetComponent<Animator>().GetBool("Ball_Fade"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_Fade", false);
        }
    }
}
