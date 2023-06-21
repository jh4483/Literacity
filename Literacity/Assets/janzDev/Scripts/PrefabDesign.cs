using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabDesign : MonoBehaviour
{
    public GameObject blankPrefab;
    private Vector2 newSizeDelta;
    private RectTransform rectTransform;
    public bool startingPosChanged;

    void Start()
    {
        rectTransform = blankPrefab.GetComponent<RectTransform>();
        newSizeDelta = rectTransform.sizeDelta;
        rectTransform.sizeDelta = newSizeDelta;
    }

    void Update()
    {
        
    }

    public IEnumerator AdjustUI()
    {
        for(int i = 0; i < SpreadSheetAccess.correctAnswers.Count; i++)
        {
            if(SpreadSheetAccess.correctAnswers[i].Length > 1)
            {
                newSizeDelta.x = 7f;
                startingPosChanged = false;
            }

            else
            {
                newSizeDelta.x = 6f;
                startingPosChanged = true;
            }
        }

        for(int i = 0; i < SpreadSheetAccess.correctAnswers.Count; i++)
        {
            if(SpreadSheetAccess.correctAnswers[i].Length > 1)
            {
                newSizeDelta.x = 7f;
                startingPosChanged = false;
            }

            else
            {
                newSizeDelta.x = 6f;
                startingPosChanged = true;
            }
        }

        yield break;
    }


}
