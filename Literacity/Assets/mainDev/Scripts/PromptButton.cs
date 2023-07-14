using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptButton : MonoBehaviour
{
    public int targetIndex;
    public int selectedIndex;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SaveTargetIndex()
    {
        var selectedNumber = name.Substring(1, 1);
        selectedIndex = int.Parse(selectedNumber) - 1;
    }
}
