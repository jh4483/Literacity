using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetCheck : MonoBehaviour
{
    public static int answerIndex = 0;

    public static void CheckTarget()
    {
        if(SpreadSheetAccess.optionsList[BasketballLauncher.saveTargetIndex] == SpreadSheetAccess.correctAnswers[answerIndex])
        {
            SpreadSheetAccess.fillableAnswers[answerIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SpreadSheetAccess.correctAnswers[answerIndex].ToString();
            answerIndex++;
        }

        else 
        {
            // Need to figure out what needs to be done
        }
    }
}
