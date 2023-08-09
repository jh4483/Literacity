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
    public GameObject introScene;

    void Start()
    {
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
    }


    void Update()
    {
        
    }

    public void OnPlay()
    {
        introScene.SetActive(false);
        StartCoroutine(spreadSheetNew.LoadRoundData());
        kazBasketball.gameObject.SetActive(true);
        cardMask.SetActive(true);
        basketballHighlight.gameObject.SetActive(true);
        StartCoroutine(DisablePlay());
    }

    private IEnumerator DisablePlay()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<Button>().enabled = false;
    }
}
