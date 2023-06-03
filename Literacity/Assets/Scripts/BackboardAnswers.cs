using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackboardAnswers : MonoBehaviour
{
    private SpreadSheetAccess spreadSheetAccess;

    private void Start()
    {
        spreadSheetAccess = FindObjectOfType<SpreadSheetAccess>();
    }

    public void BackboardClick()
    {
        if(SpreadSheetAccess.correctAnswers[SpreadSheetAccess.guessedAnswer] == gameObject.name)
        {
            
            // This is where we need to incorporate the drag functionality of the ball
            SpreadSheetAccess.fillableAnswers[SpreadSheetAccess.guessedAnswer].transform.GetChild(0).GetComponent<Text>().text = SpreadSheetAccess.correctAnswers[SpreadSheetAccess.guessedAnswer];
            SpreadSheetAccess.guessedAnswer++;

            if(SpreadSheetAccess.guessedAnswer == SpreadSheetAccess.correctAnswers.Count)
            {
                SpreadSheetAccess.ClearAllLists();
                SpreadSheetAccess.currentRound++;
                SpreadSheetAccess.guessedAnswer = 0;
                StartCoroutine(spreadSheetAccess.Start());
            }
        }
    }
}
