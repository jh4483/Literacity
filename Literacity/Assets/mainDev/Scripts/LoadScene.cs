using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{

    public GameObject[] backboardPrefab;
    public GameObject blueStrip;
    public GameObject basketBall;
    public Image wordImage;
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
        spreadsheet.StartCoroutine(spreadsheet.LoadRoundData());
        yield return new WaitForSeconds(1);
        for(int i =0; i < backboardPrefab.Length; i++)
        {
            backboardPrefab[i].SetActive(true);
        }
        blueStrip.SetActive(true);
        wordImage.gameObject.SetActive(true);
        basketBall.SetActive(true);
    }


}