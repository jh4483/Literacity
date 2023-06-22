using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{

    public GameObject[] backboardPrefab;
    public GameObject blueStrip;
    public GameObject basketBall;
    public RawImage wordImage;
    SpreadSheetAccess spreadsheet;

    void Start()
    {
        spreadsheet = FindObjectOfType<SpreadSheetAccess>();
    }


    public void PlayClicked()
    {
        StartCoroutine(LoadMain());
    }

    public IEnumerator LoadMain()
    {
        for(int i =0; i < backboardPrefab.Length; i++)
        {
            backboardPrefab[i].SetActive(true);
        }
        blueStrip.SetActive(true);
        wordImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        basketBall.SetActive(true);
        spreadsheet.StartCoroutine(spreadsheet.LoadRoundData());
    }


}