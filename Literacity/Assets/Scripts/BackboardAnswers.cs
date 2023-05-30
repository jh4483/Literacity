using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackboardAnswers : MonoBehaviour
{

    public void BackboardClick()
    {
        if(SpreadSheetAccess.correctAnswers[SpreadSheetAccess.guessedAnswer] == gameObject.name)
        {
            SpreadSheetAccess.guessedAnswer++;
        }
    }
}
