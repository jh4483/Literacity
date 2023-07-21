using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedPrompt : MonoBehaviour
{
    public GameObject cardParent;
    public GameObject backboardHighlight;
    private Color currentColour;
    private Color orginalColour;
    SpreadSheetNew spreadSheetNew;

    void Start()
    {
        backboardHighlight = GameObject.Find("Backboard Highlight");
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
        cardParent = GameObject.Find("Card Mask");
        orginalColour = backboardHighlight.GetComponent<Image>().color;
        currentColour = this.GetComponent<Image>().color;
    }

    void Update()
    {
        
    }

    public void OnPromptClicked()
    {
        backboardHighlight.GetComponent<Image>().color = currentColour;
        spreadSheetNew.targetIndex = int.Parse(gameObject.name);
        spreadSheetNew.selectedCard = cardParent.transform.Find((spreadSheetNew.targetIndex).ToString() + " Card");
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
