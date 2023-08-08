using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterState : MonoBehaviour
{
    public static int boosterPower;
    public ParticleSystem particleSystem = new ParticleSystem();
    public SpreadSheetNew spreadSheetNew;

    public Gradient[] presetColors; 

    void Start()
    {
        boosterPower = 0;
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
    }

    public void PlayParticles()
    {
        if (boosterPower == 1)
        {
            var main = particleSystem.main;
            main.startColor = presetColors[spreadSheetNew.targetIndex];
            particleSystem.Play();
        }
    }
}

