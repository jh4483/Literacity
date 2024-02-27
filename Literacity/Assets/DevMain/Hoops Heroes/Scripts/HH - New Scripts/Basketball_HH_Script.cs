using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball_HH_Script : MonoBehaviour
{
    [SerializeField]
    private Vector3 originalPos;
    BallObserver_HH_Script ballObserver;
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
        yield return new WaitForSeconds(2);
        originalPos = this.transform.localPosition;
    }

    private IEnumerator ResetBallPos()
    {
        yield return new WaitForSeconds(1);
        this.transform.localPosition = originalPos;
    }


}
