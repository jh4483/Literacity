using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private AudioSource introAudio;
    public Animator animator;
    private int introCount;

    void Start()
    {
        introCount = 0;
    }

    public void OnMouseDown()
    {
        introCount++;
        if(introCount == 1)
        {
            StartCoroutine(LoadIntroScene());
        }

        else return;
    }
    IEnumerator LoadIntroScene()
    {
        introAudio.Play();
        playButton.SetActive(false);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
        transform.GetChild(1).gameObject.SetActive(true);   

        yield return new WaitForSeconds(3f);
        transform.GetChild(2).gameObject.SetActive(true);  
        
        yield return new WaitForSeconds(1.9f);
        transform.GetChild(3).gameObject.SetActive(true); 

        yield return new WaitForSeconds(1.4f);
        transform.GetChild(4).gameObject.SetActive(true);  
        
        yield return new WaitForSeconds(1.4f);
        transform.GetChild(5).gameObject.SetActive(true); 
        
        if(!animator.GetBool("IntroDone"))
        {
            animator.SetBool("IntroDone", true);
        }

        playButton.SetActive(true);

    }
}
