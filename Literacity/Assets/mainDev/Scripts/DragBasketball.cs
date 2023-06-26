using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragBasketball : MonoBehaviour
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
    public TargetCheck targetChecker;

    void Start()
    {
        targetChecker = FindObjectOfType<TargetCheck>();
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);            //converts mouse position to screenspace

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, startLayer))            //checks for a raycast hit
            {
                startDragPoint = hit.point;
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
                Debug.Log(difference); 

                if(difference <= minimumDragDistance)
                {
                    launcher.Launch();
                    targetChecker.StartCoroutine(targetChecker.CheckTarget());
                }
            }
        }
    }
}
