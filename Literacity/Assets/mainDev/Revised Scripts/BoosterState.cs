using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterState : MonoBehaviour
{
    public static int boosterPower;
    public ParticleSystem particleSystem;
    public SpreadSheetNew spreadSheetNew;

    public Gradient[] presetColors; 

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
        if (boosterPower == 1)
        {
            var main = particleSystem.main;
            var randomColors = new ParticleSystem.MinMaxGradient(presetColors[spreadSheetNew.targetIndex]);
            randomColors.mode = ParticleSystemGradientMode.RandomColor;
            main.startColor = randomColors;
            particleSystem.Play();
        }
    }
}

