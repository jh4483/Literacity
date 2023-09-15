using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroScene : MonoBehaviour
{
    public GameObject outroScene;
    public GameObject outroAudio;
    void Start()
    {
        StartCoroutine(LoadOutroScene());
    }

    IEnumerator LoadOutroScene()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(4.5f);
        transform.GetChild(0).gameObject.SetActive(true);   

        yield return new WaitForSeconds(1.0f);
        transform.GetChild(1).gameObject.SetActive(true);  
        
        yield return new WaitForSeconds(1.0f);
        transform.GetChild(2).gameObject.SetActive(true); 

        yield return new WaitForSeconds(1.5f);
        transform.GetChild(3).gameObject.SetActive(true);  

    }
}
