using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetCheck : MonoBehaviour
{
    public static int answerIndex = 0;
    public static int changeRound = 1;
    public static GameObject canvas;
    SpreadSheetAccess spreadsheet;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        spreadsheet = FindObjectOfType<SpreadSheetAccess>();
    }

    public void CheckTarget()
    {
        if (SpreadSheetAccess.optionsList[BasketballLauncher.saveTargetIndex] == SpreadSheetAccess.correctAnswers[answerIndex])
        {
            SpreadSheetAccess.fillableAnswers[answerIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SpreadSheetAccess.correctAnswers[answerIndex].ToString();
            Color greenColor =  new Color(63f / 255f, 103f / 255f, 70f / 255f, 1f);
            SpreadSheetAccess.fillableAnswers[answerIndex].GetComponent<Image>().color = greenColor;
            StartCoroutine(spreadsheet.ClearAllLists());

        }
        else if (SpreadSheetAccess.optionsList[BasketballLauncher.saveTargetIndex] != SpreadSheetAccess.correctAnswers[answerIndex])
        {
            {
                // Debug.Log(BasketballLauncher.saveTargetIndex.gameObject.name);
            }
        }
    }
}
