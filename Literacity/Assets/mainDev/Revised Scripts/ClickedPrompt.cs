using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedPrompt : MonoBehaviour
{
    SpreadSheetNew spreadSheetNew;

    void Start()
    {
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
    }

    void Update()
    {
        
    }

    public void OnPromptClicked()
    {
        spreadSheetNew.targetIndex = int.Parse(gameObject.name);
    }
}
