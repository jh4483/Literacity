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
    [SerializeField]
    private GameObject[] hotspotButtons;
    private Dictionary<GameObject, GameObject> hotspotLink = new Dictionary<GameObject, GameObject>();
    private ObtainData_HH_Script obtainData;

    void Start()
    {
        obtainData = FindObjectOfType<ObtainData_HH_Script>();
        InitializeHotspotLinks();
    }

    void Update()
    {
        
    }

    private void InitializeHotspotLinks()
    {
        for (int i = 0; i < buttonArray.Length; i++)
        {
            hotspotLink.Add(buttonArray[i], hotspotButtons[i]);
            Debug.Log(hotspotLink[buttonArray[i]]);
        }
   
    }

    public void AddBallData()
    {
        for (int i = 0; i < obtainData.ballValues.Count; i++)
        {
            ballCollection[i].GetComponent<TextMeshProUGUI>().text = obtainData.ballValues[i];
        }
    }

    public void AddButtonData()
    {
        for (int i = 0; i < obtainData.buttonValues.Count; i++)
        {
            buttonCollection.Add(buttonArray[i], obtainData.buttonValues[i]);
        }
    }
}
