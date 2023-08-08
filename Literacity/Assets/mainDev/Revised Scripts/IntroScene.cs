using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    public GameObject playButton;

    void Start()
    {
        StartCoroutine(LoadIntroScene());
    }


    void Update()
    {
        
    }
    IEnumerator LoadIntroScene()
    {
        playButton.SetActive(false);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.2f);
        transform.GetChild(0).gameObject.SetActive(true);   

        yield return new WaitForSeconds(2.5f);
        transform.GetChild(1).gameObject.SetActive(true);  
        
        yield return new WaitForSeconds(1.4f);
        transform.GetChild(2).gameObject.SetActive(true); 

        yield return new WaitForSeconds(1.4f);
        transform.GetChild(3).gameObject.SetActive(true);  
        
        yield return new WaitForSeconds(1.4f);
        transform.GetChild(4).gameObject.SetActive(true); 

        playButton.SetActive(true);

    }
}
