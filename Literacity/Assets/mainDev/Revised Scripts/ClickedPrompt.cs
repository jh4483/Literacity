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
        if(GameObject.FindGameObjectsWithTag("open").Length == 0)
        {
            backboardHighlight.GetComponent<Image>().color = orginalColour;
        }
    }

        public void OnPromptClicked()
    {
        spreadSheetNew.targetIndex = int.Parse(gameObject.name);
        spreadSheetNew.selectedCard = cardParent.transform.Find((spreadSheetNew.targetIndex).ToString() + " Card");
        openPrompt = GameObject.FindGameObjectWithTag("open");

        if (openPrompt != null && openPrompt == spreadSheetNew.selectedCard.gameObject)
        {
            StartCoroutine(CloseCard());
        }
        else
        {
            StartCoroutine(OpenCard());
        }
    }

    private IEnumerator CloseCard()
    {
        openPrompt.GetComponent<CardAnim>().OnDone();
        openPrompt.tag = "close";
        yield return new WaitForSeconds(0.1f);

        backboardHighlight.GetComponent<Image>().color = orginalColour;
    }

    private IEnumerator OpenCard()
    {
        // yield return new WaitForSeconds(0.1f);
        // backboardHighlight.GetComponent<Image>().color = currentColour;

        if (openPrompt != null)
        {
            openPrompt.GetComponent<CardAnim>().OnDone();
            openPrompt.tag = "close";
            yield return new WaitForSeconds(0.05f);
        }

        spreadSheetNew.selectedCard.GetComponent<CardAnim>().OnSelected();
        spreadSheetNew.selectedCard.tag = "open";
    }
}