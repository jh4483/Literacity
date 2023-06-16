using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{

    SpreadSheetAccess spreadsheet;
    public GameObject[] backboardPrefabs;
    public GameObject blueStrip;
    public RawImage wordImage;
    public GameObject basketballLauncher;
    public bool isCalled;

    void Start()
    {
        isCalled = false;
        spreadsheet = FindObjectOfType<SpreadSheetAccess>();
    }
    public void LoadData()
    {
        for(int i = 0; i < backboardPrefabs.Length; i++)
        {
            backboardPrefabs[i].SetActive(true);
        }
        blueStrip.gameObject.SetActive(true);
        wordImage.gameObject.SetActive(true);
        StartCoroutine(spreadsheet.LoadRoundData());
        StartCoroutine(BasketballAppears());
    } 

    public IEnumerator BasketballAppears()
    {
        yield return new WaitForSeconds(2);
        basketballLauncher.SetActive(true);
    }
}
