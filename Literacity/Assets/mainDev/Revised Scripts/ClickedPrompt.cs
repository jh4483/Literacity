using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedPrompt : MonoBehaviour
{
    SpreadSheetNew spreadSheetNew;
    public GameObject cardParent;

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
        Transform selectedCard = cardParent.transform.GetChild(spreadSheetNew.targetIndex);   

        selectedCard.GetComponent<CardAnim>().OnSelected();    
    }
}
