using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromptInstantiation : MonoBehaviour
{
    public Button imagePrompt;
    public RectTransform background;
    public List<Button> clueCards = new List<Button>();
    SpreadSheetNew spreadSheetNew;

    void Start()
    {
        StartCoroutine(ArrangePrompts());
    }

    void Update()
    {
        
    }

    private IEnumerator ArrangePrompts()
    {
        yield return new WaitForSeconds(2);
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
        for(int i = 0; i < spreadSheetNew.words.Count; i++)
        {
            Button clueCard = Instantiate(imagePrompt, background);
            clueCard.transform.SetParent(background);
            clueCards.Add(clueCard);
            float xOffset = 110f;
            clueCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150f + (i * xOffset), -154f);
        }
        for(int i = 0; i < clueCards.Count; i++)
        {
            clueCards[i].name = spreadSheetNew.roundNumber[i];
        }
    }
}
