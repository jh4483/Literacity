using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroScene : MonoBehaviour
{
    public GameObject outroScene;
    public GameObject outroAudio;

    public ParticleSystem[] particles;
    public float time = 0.1f;
    public float repeatRate = 0.3f;
    void Start()
    {
        StartCoroutine(LoadOutroScene());
        InvokeRepeating("LoopParticles", time, repeatRate);
    }

    IEnumerator LoadOutroScene()
    {
        GetComponent<AudioSource>().Play();
        LoopParticles();
        yield return new WaitForSeconds(4.5f);
        transform.GetChild(0).gameObject.SetActive(true);   

        yield return new WaitForSeconds(1.0f);
        transform.GetChild(1).gameObject.SetActive(true);  
        
        yield return new WaitForSeconds(1.0f);
        transform.GetChild(2).gameObject.SetActive(true); 

        yield return new WaitForSeconds(1.5f);
        transform.GetChild(3).gameObject.SetActive(true);  

    }

    void LoopParticles()
    {
        int count = Random.Range(0, particles.Length);
        particles[count].Play();
    }
}
