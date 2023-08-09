using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterState : MonoBehaviour
{
    public static int boosterPower;
    public ParticleSystem particleSystem;
    public SpreadSheetNew spreadSheetNew;
    public Animator animator;
    public GameObject origin;
    public GameObject cardMask;

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
            yield return new WaitForSeconds(1.5f);
            origin.SetActive(false);
            cardMask.SetActive(false);

            yield return new WaitForSeconds(1.5f);
            KazShoots();

            yield return new WaitForSeconds(2f);

            var main = particleSystem.main;
            var randomColors = new ParticleSystem.MinMaxGradient(presetColors[spreadSheetNew.targetIndex]);
            randomColors.mode = ParticleSystemGradientMode.RandomColor;
            main.startColor = randomColors;
            particleSystem.Play();

            yield return new WaitForSeconds(3f);
            origin.SetActive(true);
            cardMask.SetActive(true);
        }
    }
}

