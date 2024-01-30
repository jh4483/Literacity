using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaz_test : MonoBehaviour, IObserver
{
    [SerializeField] Subject subject;
    void OnEnable()
    {
        subject.AddObserver(this);
    }

    void OnDisable()
    {
        subject.RemoveObserver(this);
    }

    void OnNotify()
    {
        Debug.Log("Kaz_test: OnNotify");
    }
}
