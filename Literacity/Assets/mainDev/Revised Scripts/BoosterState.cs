using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterState : MonoBehaviour
{

    public static int boosterPower;
    public ParticleSystem winningParticles;
    SpreadSheetNew spreadSheetNew;

    void Start()
    {
        boosterPower = 0;
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
    }

    void Update()
    {

    }

    public void PlayParticles()
    {
        if(boosterPower >= 1)
        {
            winningParticles.Play();
        }
    }
}
