using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackboardController : MonoBehaviour
{
    public ParticleSystem backboardParticles;

    void PlayScoreParticles()
    {
        backboardParticles.Play();
    }

    void OnEnable()
    {
       HH_Ball.onBallHoop += PlayScoreParticles; 
    }

    void OnDisable() 
    {
       HH_Ball.onBallHoop -= PlayScoreParticles; 
    }
}
