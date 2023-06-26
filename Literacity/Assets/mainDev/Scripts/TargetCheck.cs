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
    public static bool targetChecked = true;
    SpreadSheetAccess spreadsheet;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        spreadsheet = FindObjectOfType<SpreadSheetAccess>();
    }

    public IEnumerator CheckTarget()
    {
        if (SpreadSheetAccess.optionsList[BasketballLauncher.saveTargetIndex] == SpreadSheetAccess.correctAnswers[answerIndex])
        {
            targetChecked = true;
            Color originalColor =  BasketballLauncher.hitBackboard.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color;
            Color greenColor =  new Color(63f / 255f, 103f / 255f, 70f / 255f, 1f);
            BasketballLauncher.hitBackboard.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = greenColor;
            SpreadSheetAccess.fillableAnswers[answerIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SpreadSheetAccess.correctAnswers[answerIndex].ToString();
            SpreadSheetAccess.fillableAnswers[answerIndex].GetComponent<Image>().color = greenColor;
            StartCoroutine(spreadsheet.ClearAllLists());
            yield return new WaitForSeconds(1);
            BasketballLauncher.hitBackboard.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = originalColor;

        }
        else if (SpreadSheetAccess.optionsList[BasketballLauncher.saveTargetIndex] != SpreadSheetAccess.correctAnswers[answerIndex])
        {
            {
                targetChecked = false;
                Color originalColor =  BasketballLauncher.hitBackboard.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color;
                BasketballLauncher.hitBackboard.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = Color.red;
                yield return new WaitForSeconds(2);
                BasketballLauncher.hitBackboard.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = originalColor;

                //resets target checked to true so that trajectory shows to ring after a missed shot
                targetChecked = true;
            }
        }
    }
}
