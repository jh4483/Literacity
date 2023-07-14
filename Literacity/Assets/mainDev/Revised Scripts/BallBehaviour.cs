using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public Vector2 initialPos;
    public Quaternion initialRot;

    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.name == "Ring")
        {
            transform.position = initialPos;
            transform.rotation = initialRot;
        }
    }
}

