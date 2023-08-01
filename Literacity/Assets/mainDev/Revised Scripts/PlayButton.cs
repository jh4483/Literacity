using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    SpreadSheetNew spreadSheetNew;
    public Image kazBasketball;
    public GameObject cardMask;
    public Image basketballHighlight;

    void Start()
    {
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
    }


    void Update()
    {
        
    }

    public void OnPlay()
    {
        StartCoroutine(spreadSheetNew.LoadRoundData());
        kazBasketball.gameObject.SetActive(true);
        cardMask.SetActive(true);
        basketballHighlight.gameObject.SetActive(true);
    }
}
