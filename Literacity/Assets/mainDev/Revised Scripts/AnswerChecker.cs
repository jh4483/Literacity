using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerChecker : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collider) 
    {
        if(collider.gameObject.name == "Ring")
        {
            collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
