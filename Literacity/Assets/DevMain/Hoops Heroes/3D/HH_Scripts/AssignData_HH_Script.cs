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
    private AnswerChecker_HH_Script answerChecker;

    void Start()
    {
        obtainData = FindObjectOfType<ObtainData_HH_Script>();
        answerChecker = FindObjectOfType<AnswerChecker_HH_Script>();
        InitializeHotspotLinks();
    }

    void Update()
    {
        HotSpotActivated();
    }

    private void InitializeHotspotLinks()
    {
        for (int i = 0; i < buttonArray.Length; i++)
        {
            hotspotLink.Add(hotspotButtons[i], buttonArray[i]);
        }
   
    }

    public void HotSpotActivated()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.tag == "hotspot")
            {
                GameObject linkedButton = hotspotLink[hit.collider.gameObject];
                if (linkedButton != null)
                {
                    linkedButton.GetComponent<Button>().Select();
                    answerChecker.OnButtonClick();
                }
            }
        }
    }

    public void AddBallData()
    {
        for (int i = 0; i < obtainData.ballValues.Count; i++)
        {
            ballCollection[i].GetComponent<TextMeshProUGUI>().text = obtainData.ballValues[i];
            // Debug.Log("Adding Ball Data" + ballCollection[i].GetComponent<TextMeshProUGUI>().text);
        }
    }

    public void AddButtonData()
    {
        for (int i = 0; i < obtainData.buttonValues.Count; i++)
        {
            buttonCollection.Add(buttonArray[i], obtainData.buttonValues[i]);
            Debug.Log("Adding Button Data" + buttonCollection[buttonArray[i]]);
        }
    }
}
