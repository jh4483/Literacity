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
        StartCoroutine(AdjustUI());
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
                rectTransform = blankPrefab.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(6.7f, 3.2f);
                startingPosChanged = false;
            }
            else if(SpreadSheetAccess.correctAnswers[i].Length == 1)
            {
                rectTransform = blankPrefab.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(5.2f, 3.2f);
                startingPosChanged = true;
            }
            yield return null; 
        }
    }
}
