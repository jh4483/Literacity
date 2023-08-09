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

        if(animator.GetBool("Kaz_Shoot"))
        {
            animator.SetBool("Kaz_Shoot", false);
        }
    }

    public void KazLayUp()
    {
        if(!animator.GetBool("Kaz_DribbleN'Shoot"))
        {
            animator.SetBool("Kaz_DribbleN'Shoot", true);
        }

        if(animator.GetBool("Kaz_DribbleN'Shoot"))
        {
            animator.SetBool("Kaz_DribbleN'Shoot", false);
        }

    }

    public void KazDisappearL()
    {
        if(!animator.GetBool("Kaz_DisappearL"))
        {
            animator.SetBool("Kaz_DisappearL", true);
        }

        if(animator.GetBool("Kaz_DisappearL"))
        {
            animator.SetBool("Kaz_DisappearL", false);
        }

    }

    public void KazDisappearR()
    {
        if(!animator.GetBool("Kaz_DisappearR"))
        {
            animator.SetBool("Kaz_DisappearR", true);
        }

        if(animator.GetBool("Kaz_DisappearR"))
        {
            animator.SetBool("Kaz_DisappearR", false);
        }

    }

    public void KazFade()
    {
        if(!animator.GetBool("Kaz_Fade"))
        {
            animator.SetBool("Kaz_Fade", true);
        }

        if(animator.GetBool("Kaz_Fade"))
        {
            animator.SetBool("Kaz_Fade", false);
        }

    }

    public void KazDunk()
    {
        if(!animator.GetBool("Kaz_Dunk"))
        {
            animator.SetBool("Kaz_Dunk", true);
        }

        if(animator.GetBool("Kaz_Dunk"))
        {
            animator.SetBool("Kaz_Dunk", false);
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

            KazShoots();

            origin.SetActive(true);
            cardMask.SetActive(true);
        }

        // switch (boosterPower)
        // {
        //     case 1:
        //         var main = particleSystem.main;
        //         var randomColors = new ParticleSystem.MinMaxGradient(presetColors[spreadSheetNew.targetIndex]);
        //         randomColors.mode = ParticleSystemGradientMode.RandomColor;
        //         main.startColor = randomColors;
        //         particleSystem.Play();
        //         break;
            
        //     case 2:

        //         break;
            
        //     case 3:
                
        //         break;

        //     case 4:
                
        //         break;

        //     case 5:

        //         break;
            
        //     case 6:

        //         break;

        //     case 7:

        //         break;

        //     case 8:
        
        //         break;

        //     default:
        //         break;
        // }
    }
}

