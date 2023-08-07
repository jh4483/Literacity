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
        if (GameObject.FindGameObjectsWithTag("open").Length == 0)
        {
            backboardHighlight.GetComponent<Image>().color = orginalColour;
        }
    }

    public void OnPromptClicked()
    {   
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

            StartCoroutine(StartFadeIn());

            spreadSheetNew.selectedCard.GetComponent<CardAnim>().OnSelected();
            spreadSheetNew.selectedCard.tag = "open";
        }
    }

    private IEnumerator StartFadeIn()
    {
        Color targetColor = currentColour;
        Image backboardImage = backboardHighlight.GetComponent<Image>();
        Color originalColor = backboardImage.color;

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alphaValue = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            targetColor.a = alphaValue;
            backboardImage.color = targetColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetColor.a = 1f;
        backboardImage.color = targetColor;

    }
}
