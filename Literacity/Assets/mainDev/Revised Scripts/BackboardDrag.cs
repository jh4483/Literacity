using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackboardDrag : MonoBehaviour 
{
    public Image backboardImage;
    public float width = 10f;
    public bool isColliding = false;

    void Start()
    {

    }

    void Update()
    {
        CustomCollisionCheck();
    }

    void CustomCollisionCheck()
    {
        // if( Input.mousePosition.x > backboardImage.rectTransform.position.x - backboardImage.rectTransform.rect.width / 2 &&
        //     Input.mousePosition.x < backboardImage.rectTransform.position.x - backboardImage.rectTransform.rect.width / 2 + (width) ||
        //     Input.mousePosition.x < backboardImage.rectTransform.position.x + backboardImage.rectTransform.rect.width / 2 &&
        //     Input.mousePosition.x > backboardImage.rectTransform.position.x + backboardImage.rectTransform.rect.width / 2 - (width) &&
        //     Input.mousePosition.y > backboardImage.rectTransform.position.y - backboardImage.rectTransform.rect.height /2 &&
        //     Input.mousePosition.y < backboardImage.rectTransform.position.y - backboardImage.rectTransform.rect.height /2 + (width) ||
        //     Input.mousePosition.y < backboardImage.rectTransform.position.y + backboardImage.rectTransform.rect.height /2 &&
        //     Input.mousePosition.y > backboardImage.rectTransform.position.y + backboardImage.rectTransform.rect.height /2 - (width)
        //     )
        // {
        //     Debug.Log("HIT");
        // }

        // if( Input.mousePosition.x > backboardImage.rectTransform.position.x - backboardImage.rectTransform.rect.width / 2 &&
        //     Input.mousePosition.x < backboardImage.rectTransform.position.x - backboardImage.rectTransform.rect.width / 2 + (width)
        //     )
        // {
        //     Debug.Log("Collision Left");
        // }

        // if( Input.mousePosition.x < backboardImage.rectTransform.position.x + backboardImage.rectTransform.rect.width / 2 &&
        //     Input.mousePosition.x > backboardImage.rectTransform.position.x + backboardImage.rectTransform.rect.width / 2 - (width)
        //     )
        // {
        //     Debug.Log("Collision Right");
        // }

        // if( Input.mousePosition.y > backboardImage.rectTransform.position.y - backboardImage.rectTransform.rect.height /2 &&
        //     Input.mousePosition.y < backboardImage.rectTransform.position.y - backboardImage.rectTransform.rect.height /2 + (width)
        //     )
        // {
        //     Debug.Log("Collision Bottom");            
        // }
        // if( Input.mousePosition.y < backboardImage.rectTransform.position.y + backboardImage.rectTransform.rect.height /2 &&
        //     Input.mousePosition.y > backboardImage.rectTransform.position.y + backboardImage.rectTransform.rect.height /2 - (width)
        //     )
        // {
        //     Debug.Log("Collision Top");
        // }

        if( Input.mousePosition.x > backboardImage.rectTransform.position.x - backboardImage.rectTransform.rect.width / 2 &&
            Input.mousePosition.x < backboardImage.rectTransform.position.x + backboardImage.rectTransform.rect.width / 2 &&
            Input.mousePosition.y > backboardImage.rectTransform.position.y - backboardImage.rectTransform.rect.height /2 &&
            Input.mousePosition.y < backboardImage.rectTransform.position.y + backboardImage.rectTransform.rect.height /2)
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }
    }
}