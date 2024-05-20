using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class AssignData_HH_Script : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> ballCollection = new List<GameObject>();
    [SerializeField]
    public GameObject[] buttonArray;
    public Dictionary<GameObject, string> buttonCollection = new Dictionary<GameObject, string>();
    [SerializeField]
    private GameObject[] hotspotButtons;
    [SerializeField]
    public GameObject gameCam;
    [SerializeField]
    public GameObject basketBallPost;
    [SerializeField]
    public GameObject kazPlayer;
    [SerializeField]
    public Transform hotspotPos;
    private Dictionary<GameObject, GameObject> hotspotLink = new Dictionary<GameObject, GameObject>();
    private ObtainData_HH_Script obtainData;
    private AnswerChecker_HH_Script answerChecker;
    private bool isMoving = false;

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
                // GameObject linkedButton = hotspotLink[hit.collider.gameObject];
                // if (linkedButton != null)
                // {
                //     linkedButton.GetComponent<Button>().Select();
                    // answerChecker.OnButtonClick();                 
                    hotspotPos = hit.collider.transform;
                    GameObject hotSpotAnim = hit.collider.gameObject;
                    if(!isMoving)
                    {
                        StartCoroutine(MoveKazToHotspot(hotSpotAnim, hotspotPos));
                    }

                // }
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

    private IEnumerator MoveKazToHotspot(GameObject Collider, Transform Switch)
    {
        isMoving = true;
        kazPlayer.GetComponent<Animator>().SetBool("toMove", true);

        while(kazPlayer.transform.position != Switch.position)
        {
            kazPlayer.transform.LookAt(Switch.position);
            kazPlayer.transform.position = Vector3.Lerp(kazPlayer.transform.position, Switch.position, 0.1f);

            if(Mathf.Abs(Vector3.Distance(kazPlayer.transform.position, Switch.transform.position)) < 0.5f)
            {
                kazPlayer.GetComponent<Animator>().SetBool("toMove", false);   
                kazPlayer.transform.position = Switch.transform.position;
                
                Collider.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(0, 1, 1, 0);

                Collider.transform.GetChild(0).GetComponent<Animation>().Play();
                break;
            }

            yield return new WaitForSeconds(0.05f);            
        }


        kazPlayer.transform.LookAt(basketBallPost.transform.position);
        gameCam.SetActive(true);
        isMoving = false;

        yield return null;
    }
}
