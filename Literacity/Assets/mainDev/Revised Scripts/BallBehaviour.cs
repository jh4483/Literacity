using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    Vector2 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.name == "Upper Ground")
        {
            transform.position = initialPos;
        }
    }
}

