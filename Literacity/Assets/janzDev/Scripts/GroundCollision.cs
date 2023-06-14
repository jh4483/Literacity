using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            
        }
    }
}
