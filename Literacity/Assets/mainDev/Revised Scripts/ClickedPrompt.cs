using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedPrompt : MonoBehaviour
{
    public GameObject cardParent;
    public GameObject backboardHighlight;
    public GameObject openPrompt;
    private Color currentColour;
    private Color orginalColour;
    SpreadSheetNew spreadSheetNew;
    PlayButton playButton;
    MoveButton moveButton;

    void Start()
    {
        backboardHighlight = GameObject.Find("Backboard Highlight");
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
        cardParent = GameObject.Find("Card Mask");
        orginalColour = backboardHighlight.GetComponent<Image>().color;
        currentColour = this.GetComponent<Image>().color;
        moveButton = FindObjectOfType<MoveButton>();
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("open").Length == 0)
        {
            backboardHighlight.GetComponent<Image>().color = orginalColour;
        }
    }

    public void OnPromptClicked()
    {   
        moveButton.movedButtons.Add(gameObject);
        moveButton.TransformButton();
        backboardHighlight.GetComponent<Image>().color = currentColour;
        spreadSheetNew.targetIndex = int.Parse(gameObject.name);
        spreadSheetNew.selectedCard = cardParent.transform.Find((spreadSheetNew.targetIndex).ToString() + " Card");
        openPrompt = GameObject.FindGameObjectWithTag("open");

        if (openPrompt == spreadSheetNew.selectedCard.gameObject)
        {
            spreadSheetNew.selectedCard.GetComponent<CardAnim>().OnDone();
            spreadSheetNew.selectedCard.tag = "close";
        }
        else
        {
            if (openPrompt != null)
            {
                openPrompt.GetComponent<CardAnim>().OnDone();
                openPrompt.tag = "close";
            }

                Color targetColor = currentColour;
                Image backboardImage = backboardHighlight.GetComponent<Image>();
                Color originalColor = backboardImage.color;
                backboardImage.color = targetColor;

            spreadSheetNew.selectedCard.GetComponent<CardAnim>().OnSelected();
            spreadSheetNew.selectedCard.tag = "open";
        }
    }
}
