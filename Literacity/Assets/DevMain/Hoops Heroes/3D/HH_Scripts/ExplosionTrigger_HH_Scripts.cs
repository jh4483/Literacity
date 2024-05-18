using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger_HH_Scripts : MonoBehaviour
{
    public delegate void BallExplosionEvent();
    public static event BallExplosionEvent onBallExplode;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ring"))
        {
            onBallExplode();
            Debug.Log("Triggered");
        }
    }
}
