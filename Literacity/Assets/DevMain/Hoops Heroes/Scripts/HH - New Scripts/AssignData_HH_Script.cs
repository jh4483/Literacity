using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignData_HH_Script : MonoBehaviour
{
    public List<GameObject> ballCollection = new List<GameObject>();
    ObtainData_HH_Script obtainData;
    void Start()
    {
        obtainData = FindObjectOfType<ObtainData_HH_Script>();
    }

    void Update()
    {
        
    }

    public void AddBallData()
    {
        for(int i = 0; i < obtainData.ballValues.Count; i++)
        {
            ballCollection[i].GetComponent<TextMeshProUGUI>().text = obtainData.ballValues[i];
        }
    }
}
