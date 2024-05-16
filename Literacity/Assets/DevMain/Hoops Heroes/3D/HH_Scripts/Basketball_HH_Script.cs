using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Basketball_HH_Script : MonoBehaviour
{
    [SerializeField]
    private Vector3 originalPos;
    BallObserver_HH_Script ballObserver;

    //Ball VFX badly done Observer Pattern. Need to rework this.
    public delegate void BallHoopEvent();
    public static event BallHoopEvent onBallHoop;
    private string ballText;
    private float heightChecker;

    [SerializeField] private float heightMaxVal = 16;
    void Start()
    {
        StartCoroutine(SaveBallPos());
        ballObserver = FindObjectOfType<BallObserver_HH_Script>();
        ballObserver.moveOver.AddListener(()=> StartCoroutine(ResetBallPos()));
    }

    void Update()
    {
        
    }

    private IEnumerator SaveBallPos()
    {
        yield return new WaitForSeconds(2.5f);
        originalPos = this.transform.localPosition;
    }

    private IEnumerator ResetBallPos()
    {
        yield return new WaitForSeconds(1);
        this.transform.localPosition = originalPos;
    }

    //Ball VFX badly done Observer Pattern. Need to rework this.
    void OnTriggerEnter(Collider other)
    {
        heightChecker = this.transform.localPosition.y;
        if(other.gameObject.tag == "Ring" && onBallHoop != null && heightChecker > heightMaxVal)
        {
            Debug.Log("Ball Hooped");
            ballText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
            ballObserver.pickedWord = ballText;
            onBallHoop();
        }  
    }


}
