using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedPrompt : MonoBehaviour
{
    public GameObject cardParent;
    SpreadSheetNew spreadSheetNew;

    void Start()
    {
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
        cardParent = GameObject.Find("Card Mask");
    }

    void Update()
    {
        
    }

    public void OnPromptClicked()
    {
        spreadSheetNew.targetIndex = int.Parse(gameObject.name);
        spreadSheetNew.selectedCard = cardParent.transform.Find((spreadSheetNew.targetIndex).ToString());
        GameObject openPrompt = GameObject.FindGameObjectWithTag("open");
        if (openPrompt != null)
        {
            openPrompt.transform.GetComponent<CardAnim>().OnDone();
            openPrompt.tag = "close";
        }
        spreadSheetNew.selectedCard.GetComponent<CardAnim>().OnSelected();
        spreadSheetNew.selectedCard.tag = "open";
    }
}
