using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallObserver_HH_Script : MonoBehaviour
{
    [SerializeField]
    public UnityEvent moveOver;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TransmitInfo()
    {
        moveOver.Invoke();
    }
}
