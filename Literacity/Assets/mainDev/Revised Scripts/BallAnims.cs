using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnims : MonoBehaviour
{
    [SerializeField] GameObject ballSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootBallL()
    {
        Debug.Log("Shoot Ball");
        ballSprite.SetActive(true);

        if(!ballSprite.GetComponent<Animator>().GetBool("Ball_Shoot_L"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_L", true);
        }

        else if(ballSprite.GetComponent<Animator>().GetBool("Ball_Shoot_L"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_L", false);
        }
    }

    public void ShootBallDribbleL()
    {
        Debug.Log("DribbleNShoot");
        ballSprite.SetActive(true);
        
        if(ballSprite.GetComponent<Animator>().GetBool("Ball_DribbleNShoot_L"))
        {
            ballSprite.GetComponent<Animator>().SetBool("Ball_DribbleNShoot_L", true);
        }

    }

    public void ShootBallDribbleR()
    {

    }
}
