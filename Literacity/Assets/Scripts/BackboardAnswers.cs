using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackboardAnswers : MonoBehaviour
{
    public void BackboardClick()
    {
        if(SpreadSheetAccess.correctAnswers[SpreadSheetAccess.guessedAnswer] == gameObject.name)
        {
            
            // This is where we need to incorporate the drag functionality of the ball

            Image image = SpreadSheetAccess.fillableAnswers[SpreadSheetAccess.guessedAnswer].gameObject.GetComponent<Image>();
            Color transparentColor = new Color(image.color.r, image.color.g, image.color.b, 0f);
            image.color = transparentColor;
            SpreadSheetAccess.fillableAnswers[SpreadSheetAccess.guessedAnswer].transform.GetChild(0).GetComponent<Text>().text = SpreadSheetAccess.correctAnswers[SpreadSheetAccess.guessedAnswer];
            SpreadSheetAccess.guessedAnswer++;

            if(SpreadSheetAccess.guessedAnswer == SpreadSheetAccess.correctAnswers.Count)
            {
                LoadLevel loadLevel = FindObjectOfType<LoadLevel>();
                StartCoroutine(loadLevel.LoadNextLevel());
            }
        }
    }
}
