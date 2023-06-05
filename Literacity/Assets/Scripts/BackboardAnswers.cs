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
        
        // correctAnswers is a list. I'm using guessed answer as indexing to check if the player has clicked on the right answer or not. I don't have a functionaility for wrong answer yet
        // For the word 'MULE', M and L are missing. so SpreadsSheetAccess.correctAnswers[0] = M. And if that is is the name of the button, the ball functionality needs to be enabled.
        if(SpreadSheetAccess.correctAnswers[SpreadSheetAccess.guessedAnswer] == gameObject.name)
        {
            // When the correct letter is clicked, the player shoots the ball. 
            // This is where we need to incorporate the drag functionality of the ball.
            
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
