using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackboardDrag : MonoBehaviour 
{
    public Image backboardImage;
    public bool isRunning = false;
    DragBall dragBall;

    void Start()
    {
        dragBall = GetComponent<DragBall>();
        backboardImage = GameObject.Find("BackboardImage").GetComponent<Image>();
    }

    void Update()
    {
        CustomCollisionCheck();
    }

    void CustomCollisionCheck()
    {
        if( Input.mousePosition.x > backboardImage.rectTransform.position.x - backboardImage.rectTransform.rect.width / 2 &&
            Input.mousePosition.x < backboardImage.rectTransform.position.x + backboardImage.rectTransform.rect.width / 2 &&
            Input.mousePosition.y > backboardImage.rectTransform.position.y - backboardImage.rectTransform.rect.height/ 2 &&
            Input.mousePosition.y < backboardImage.rectTransform.position.y + backboardImage.rectTransform.rect.height/ 2)
        {
            StartCoroutine(TurnOffDragBall());
        }
    }

    private IEnumerator TurnOffDragBall()
    {
        if(!isRunning)
        {
            isRunning = true;
            dragBall.enabled = false;
            yield return new WaitForSeconds(0.2f);
            dragBall.enabled = true;
            isRunning = false;
        }
    }
}