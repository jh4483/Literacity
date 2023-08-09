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

        else if(animator.GetBool("Kaz_Shoot"))
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

        else if(animator.GetBool("Kaz_DribbleN'Shoot"))
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

        else if(animator.GetBool("Kaz_DisappearL"))
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

        else if(animator.GetBool("Kaz_DisappearR"))
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

        else if(animator.GetBool("Kaz_Fade"))
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

        else if(animator.GetBool("Kaz_Dunk"))
        {
            animator.SetBool("Kaz_Dunk", false);
        }

    }

    public void KazDribbleTurn()
    {
        if(!animator.GetBool("Kaz_DribbleTurn"))
        {
            animator.SetBool("Kaz_DribbleTurn", true);
        }

        else if(animator.GetBool("Kaz_DribbleTurn"))
        {
            animator.SetBool("Kaz_DribbleTurn", false);
        }

    }
    

    public IEnumerator PlayParticles()
    {
        var main = particleSystem.main;
        var randomColors = new ParticleSystem.MinMaxGradient(presetColors[spreadSheetNew.targetIndex]);
        yield return new WaitForSeconds(1.5f);
        origin.SetActive(false);
        cardMask.SetActive(false);



        switch (boosterPower)
        {
            case 1:
                yield return new WaitForSeconds(1.5f);
                KazShoots();

                yield return new WaitForSeconds(2f);
                randomColors.mode = ParticleSystemGradientMode.RandomColor;
                main.startColor = randomColors;
                particleSystem.Play();

                KazShoots();

                break;
            
            case 2:
                yield return new WaitForSeconds(1.5f);
                KazShoots();

                yield return new WaitForSeconds(2f);
                randomColors.mode = ParticleSystemGradientMode.RandomColor;
                main.startColor = randomColors;
                particleSystem.Play();

                KazShoots();

                break;
            
            case 3:
                yield return new WaitForSeconds(1.5f);
                KazLayUp();

                yield return new WaitForSeconds(2f);
                randomColors.mode = ParticleSystemGradientMode.RandomColor;
                main.startColor = randomColors;
                particleSystem.Play();

                KazLayUp();
                
                break;

            case 4:
                yield return new WaitForSeconds(1.5f);
                KazDisappearR();

                yield return new WaitForSeconds(2f);
                randomColors.mode = ParticleSystemGradientMode.RandomColor;
                main.startColor = randomColors;
                particleSystem.Play();

                KazDisappearR();
                
                break;

            case 5:
                yield return new WaitForSeconds(1.5f);
                KazDribbleTurn();

                yield return new WaitForSeconds(2f);
                randomColors.mode = ParticleSystemGradientMode.RandomColor;
                main.startColor = randomColors;
                particleSystem.Play();

                KazDribbleTurn();

                break;
            
            case 6:
                yield return new WaitForSeconds(1.5f);
                KazDisappearL();

                yield return new WaitForSeconds(2f);
                randomColors.mode = ParticleSystemGradientMode.RandomColor;
                main.startColor = randomColors;
                particleSystem.Play();

                KazDisappearL();

                break;

            case 7:
                yield return new WaitForSeconds(1.5f);
                KazFade();

                yield return new WaitForSeconds(2f);
                randomColors.mode = ParticleSystemGradientMode.RandomColor;
                main.startColor = randomColors;
                particleSystem.Play();

                KazFade();

                break;

            case 8:
                yield return new WaitForSeconds(1.5f);
                KazDunk();

                yield return new WaitForSeconds(2f);
                randomColors.mode = ParticleSystemGradientMode.RandomColor;
                main.startColor = randomColors;
                particleSystem.Play();

                KazDunk();
        
                break;

            default:
                break;
        }

        yield return new WaitForSeconds(2f);
        origin.SetActive(true);
        cardMask.SetActive(true);
    }
}

