using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterState : MonoBehaviour
{
    public static int boosterPower;
    public ParticleSystem particleSystem;
    public SpreadSheetNew spreadSheetNew;
    public Animator animator;

    public Gradient[] presetColors; 

    void Start()
    {
        boosterPower = 0;
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
    }

    void Update()
    {
        
    }

    public void KazShoots()
    {
        if(!animator.GetBool("Kaz_Shoot"))
        {
            animator.SetBool("Kaz_Shoot", true);
        }
    }



    public IEnumerator PlayParticles()
    {
        if (boosterPower == 1)
        {
            KazShoots();

            yield return new WaitForSeconds(2f);
            
            var main = particleSystem.main;
            var randomColors = new ParticleSystem.MinMaxGradient(presetColors[spreadSheetNew.targetIndex]);
            randomColors.mode = ParticleSystemGradientMode.RandomColor;
            main.startColor = randomColors;
            particleSystem.Play();
        }
    }
}

