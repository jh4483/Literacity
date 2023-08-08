using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    public List <GameObject> movedButtons = new List<GameObject>();
    private Vector2 originialPos;

    void Start()
    {

    }


    void Update()
    {
        
    }

    public void TransformButton()
    {
        if(movedButtons.Count == 1)
        {
            originialPos = movedButtons[0].gameObject.GetComponent<RectTransform>().anchoredPosition;
            movedButtons[0].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(77, 175);
        }


        if(movedButtons.Count == 2)
        {
            if(movedButtons[0].name != movedButtons[1].name)
            {
                movedButtons[0].gameObject.GetComponent<RectTransform>().anchoredPosition = originialPos;
                movedButtons.RemoveAt(0);
                originialPos = movedButtons[0].gameObject.GetComponent<RectTransform>().anchoredPosition;
                movedButtons[0].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(77, 175);
            }

            else 
            {
                movedButtons[0].gameObject.GetComponent<RectTransform>().anchoredPosition = originialPos;
                movedButtons.RemoveAt(0);
                movedButtons.RemoveAt(0);
            }

        }
    }
}
