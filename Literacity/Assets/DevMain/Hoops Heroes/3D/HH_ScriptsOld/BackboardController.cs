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
       Basketball_HH_Script.onBallHoop += PlayScoreParticles; 
    }

    void OnDisable() 
    {
       Basketball_HH_Script.onBallHoop -= PlayScoreParticles; 
    }
}
