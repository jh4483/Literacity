using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HH_Ball : MonoBehaviour
{
    public delegate void BallHoopEvent();
    public static event BallHoopEvent onBallHoop;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ring" && onBallHoop != null)
        {
            Debug.Log("A HOOP!!");
            onBallHoop();
        }  
    }
}
