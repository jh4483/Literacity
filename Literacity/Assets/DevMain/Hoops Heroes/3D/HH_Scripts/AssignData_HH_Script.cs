using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignData_HH_Script : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> ballCollection = new List<GameObject>();
    [SerializeField]
    public GameObject[] buttonArray;
    public Dictionary<GameObject, string> buttonCollection = new Dictionary<GameObject, string>();
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
            // Debug.Log("Adding Ball Data" + ballCollection[i].GetComponent<TextMeshProUGUI>().text);
        }
    }

    public void AddButtonData()
    {
        for(int i = 0; i < obtainData.buttonValues.Count; i++)
        {
            buttonCollection.Add(buttonArray[i], obtainData.buttonValues[i]);
            Debug.Log("Adding Button Data" + buttonCollection[buttonArray[i]]);
        }
    }

}
