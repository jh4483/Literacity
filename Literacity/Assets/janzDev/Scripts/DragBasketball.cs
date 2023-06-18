using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragBasketball : MonoBehaviour /*, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler*/
{
    [Header("Variables")]
    public bool isPressed = false;
    [SerializeField] LayerMask startLayer;
    [SerializeField] LayerMask endLayer;
    Ray ray;
    RaycastHit hit;
    public Vector3 startDragPoint = Vector3.zero;
    public Vector3 endDragPoint = Vector3.zero;
    public float minimumDragDistance = 5f;

    [Header("References")]
    public BasketballLauncher launcher;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);            //converts mouse position to screenspace

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, startLayer))            //checks for a raycast hit
            {
                startDragPoint = hit.point;

                Debug.Log(hit.collider.name);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);            //converts mouse position to screenspace

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, endLayer))            //checks for a raycast hit
            {
                endDragPoint = hit.point;

                Debug.Log(hit.collider.name);

                float difference = endDragPoint.y - startDragPoint.y;
                difference = Mathf.Abs(difference); 
                Debug.Log(difference); 

                if(difference >= minimumDragDistance)
                {
                    launcher.Launch();
                    TargetCheck.CheckTarget();
                }
            }
        }
    }

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     Debug.Log("Pointer Down");
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     Debug.Log("Dragging");
    // }

    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //     Debug.Log("Begin Drag");
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     Debug.Log("End Drag");
    // }
}
