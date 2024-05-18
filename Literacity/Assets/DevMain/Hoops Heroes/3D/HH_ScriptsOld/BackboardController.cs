using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackboardController : MonoBehaviour
{
    public ParticleSystem backboardParticles;

    [SerializeField] private ParticleSystem particleBase;
    [SerializeField] private ParticleSystem particleVertical;

    void PlayScoreParticles()
    {
        backboardParticles.Play();
    }

    void PlayExplosion()
    {
        particleBase.Play();
        particleVertical.Play();
    }

    void OnEnable()
    {
       Basketball_HH_Script.onBallHoop += PlayScoreParticles;
       ExplosionTrigger_HH_Scripts.onBallExplode += PlayExplosion; 
    }

    void OnDisable() 
    {
       Basketball_HH_Script.onBallHoop -= PlayScoreParticles;
       ExplosionTrigger_HH_Scripts.onBallExplode -= PlayExplosion; 
    }
}
