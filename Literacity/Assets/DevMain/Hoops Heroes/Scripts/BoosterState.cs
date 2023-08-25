using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoosterState : MonoBehaviour
{
    public static int boosterPower;
    public ParticleSystem particleSystem;
    public SpreadSheetNew spreadSheetNew;
    public Animator animator;
    public GameObject origin;
    public GameObject cardMask;
    public GameObject backBoardHighlight;
    public bool isCorrect;
    public bool hasPlayedParticles;
    public bool hasCollided;
    public float timeTaken;
    private float fadeDuration = 1.0f;


    public Gradient[] presetColors;

    public BallAnims ballAnims; 

    void Start()
    {
        boosterPower = 0;
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
        hasPlayedParticles = false;

        //ballAnims = FindObjectOfType<BallAnims>();
        
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
        // if(boosterPower == 1 || boosterPower == 2)
        // {
        //     var main = particleSystem.main;
        //     var randomColors = new ParticleSystem.MinMaxGradient(presetColors[spreadSheetNew.targetIndex]);
        //     yield return new WaitForSeconds(1.5f);
        //     origin.SetActive(false);
        //     cardMask.SetActive(false);
        //     yield return new WaitForSeconds(1.5f);
        //     KazShoots();

        //     yield return new WaitForSeconds(2f);
        //     randomColors.mode = ParticleSystemGradientMode.RandomColor;
        //     main.startColor = randomColors;
        //     particleSystem.Play();

        //     KazShoots();

        //     yield return new WaitForSeconds(2f);
        //     origin.SetActive(true);
        //     cardMask.SetActive(true);
        // }
        
        
        var main = particleSystem.main;
        var randomColors = new ParticleSystem.MinMaxGradient(presetColors[spreadSheetNew.targetIndex]);
        // origin.SetActive(false);
        // cardMask.SetActive(false);

        // Become Transparent (fading)
        StartCoroutine(FadeOutImagesAndText());

        switch (boosterPower)
        {
            case 1:
                timeTaken = 4f;
                yield return new WaitForSeconds(1f);
                KazShoots();

                yield return new WaitForSeconds(2f);
                //ballAnims.ShootBallL();
                ballAnims.ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_L", false);
                ballAnims.ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_Missed", false);
                ballAnims.ballSprite.SetActive(false);
                //KazShoots();
                animator.SetBool("Kaz_Shoot", false);

                break;
            
            case 2:
                timeTaken = 4f;
                yield return new WaitForSeconds(1f);
                KazShoots();

                yield return new WaitForSeconds(2f);
                //ballAnims.ShootBallL();
                ballAnims.ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_L", false);
                ballAnims.ballSprite.GetComponent<Animator>().SetBool("Ball_Shoot_Missed", false);
                ballAnims.ballSprite.SetActive(false);
                //KazShoots();
                animator.SetBool("Kaz_Shoot", false);

                break;
            
            case 3:
                timeTaken = 5.3f;
                yield return new WaitForSeconds(1f);
                KazLayUp();

                yield return new WaitForSeconds(3.3f);
                //ballAnims.ShootBallDribbleL();
                ballAnims.ballSprite.GetComponent<Animator>().SetBool("Ball_DribbleNShoot_L", false);
                ballAnims.ballSprite.SetActive(false);
                //KazLayUp();
                animator.SetBool("Kaz_DribbleN'Shoot", false);
                
                break;

            case 4:
                timeTaken = 7.7f;
                yield return new WaitForSeconds(1f);
                KazDisappearR();

                yield return new WaitForSeconds(5.7f);
                //ballAnims.ShootBallDribbleL();
                ballAnims.ballSprite.GetComponent<Animator>().SetBool("Ball_DribbleNShoot_L", false);
                ballAnims.ballSprite.SetActive(false);                
                //KazDisappearR();
                animator.SetBool("Kaz_DisappearR", false);
                
                break;

            case 5:
                timeTaken = 7.5f;
                yield return new WaitForSeconds(1f);
                KazDribbleTurn();

                yield return new WaitForSeconds(5.5f);
                ballAnims.ballSprite.GetComponent<Animator>().SetBool("Ball_DribbleNShoot_L", false);
                ballAnims.ballSprite.SetActive(false);
                //KazDribbleTurn();
                animator.SetBool("Kaz_DribbleTurn", false);

                break;
            
            case 6:
                timeTaken = 7.7f;
                yield return new WaitForSeconds(1f);
                KazDisappearL();

                yield return new WaitForSeconds(5.7f);
                ballAnims.ballSprite.GetComponent<Animator>().SetBool("Ball_DribbleNShoot_L", false);
                ballAnims.ballSprite.SetActive(false);
                //KazDisappearL();
                animator.SetBool("Kaz_DisappearL", false);

                break;

            case 7:
                timeTaken = 5.95f;
                yield return new WaitForSeconds(1f);
                KazFade();

                yield return new WaitForSeconds(3.95f);
                ballAnims.ballSprite.GetComponent<Animator>().SetBool("Ball_Fade", false);
                ballAnims.ballSprite.SetActive(false);
                //KazFade();
                animator.SetBool("Kaz_Fade", false);

                break;

            case 8:
                timeTaken = 4.6f;
                yield return new WaitForSeconds(1f);
                KazDunk();

                yield return new WaitForSeconds(2.6f);
                //KazDunk();
                animator.SetBool("Kaz_Dunk", false);
        
                break;

            default:
                break;
        }

        randomColors.mode = ParticleSystemGradientMode.RandomColor;
        main.startColor = randomColors;
        backBoardHighlight.GetComponent<Animation>().Play("Backboard explodes");
        particleSystem.Play();
        
        yield return new WaitForSeconds(1f);

        hasPlayedParticles = true;
        hasCollided = false;
        StartCoroutine(FadeInImagesAndText());
        // origin.SetActive(true);
        // cardMask.SetActive(true);
    }


    private IEnumerator FadeOutImagesAndText()
    {
        yield return new WaitForSeconds(1);
        float elapsedTime = 0;
        float startAlpha = 1.0f;
        float targetAlpha = 0.0f;
        float fadeDuration = 0.1f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float normalizedTime = elapsedTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            foreach (Image image in origin.GetComponentsInChildren<Image>())
            {
                Color imageColor = image.color;
                imageColor.a = currentAlpha;
                image.color = imageColor;
            }
            foreach (TextMeshProUGUI textMeshPro in origin.GetComponentsInChildren<TextMeshProUGUI>())
            {
                Color textMeshColor = textMeshPro.color;
                textMeshColor.a = currentAlpha;
                textMeshPro.color = textMeshColor;
            }
            foreach (Image image in cardMask.GetComponentsInChildren<Image>())
            {
                Color imageColor = image.color;
                imageColor.a = currentAlpha;
                image.color = imageColor;
            }

            yield return null;
        }
    }

    private IEnumerator FadeInImagesAndText()
    {
        float elapsedTime = 0;
        float startAlpha = 0.0f;
        float targetAlpha = 1.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float normalizedTime = elapsedTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);

            foreach (Image image in origin.GetComponentsInChildren<Image>())
            {
                Color imageColor = image.color;
                imageColor.a = currentAlpha;
                image.color = imageColor;
            }

            foreach (TextMeshProUGUI textMeshPro in origin.GetComponentsInChildren<TextMeshProUGUI>())
            {
                Color textMeshColor = textMeshPro.color;
                textMeshColor.a = currentAlpha;
                textMeshPro.color = textMeshColor;
            }

            foreach (Image image in cardMask.GetComponentsInChildren<Image>())
            {
                Color imageColor = image.color;
                imageColor.a = currentAlpha;
                image.color = imageColor;
            }

            hasPlayedParticles = false;

            yield return null;
        }
    }
}