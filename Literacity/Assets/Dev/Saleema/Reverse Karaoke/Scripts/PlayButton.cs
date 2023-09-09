using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonPress : MonoBehaviour
{
    public GameObject button;
    public GameObject title1;
    public GameObject title2;
    public GameObject title3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void makeWordsDisappear()
    {
        if (button.activeSelf)
        {
            title1.SetActive(false);
            title2.SetActive(false);
            title3.SetActive(false);
        }
    }
}