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

    [Header("Darg Variables")]
    public Vector3 startDragPoint = Vector3.zero;
    public Vector3 endDragPoint = Vector3.zero;
    public float minimumDragDistance = 5f;

    public float dragValue = 0f;

    [Header("References")]
    public BasketballLauncher launcher;
    public TargetCheck targetChecker;

    [Header("UI References")]
    public Image dragMeter;

    void Start()
    {
        targetChecker = FindObjectOfType<TargetCheck>();

        dragMeter.fillAmount = 0f;
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

        if(Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);            //converts mouse position to screenspace

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, endLayer))            //checks for a raycast hit
            {
                endDragPoint = hit.point;

                dragValue = endDragPoint.y - startDragPoint.y;

                float normalDragValue = dragValue/minimumDragDistance;

                dragMeter.fillAmount = normalDragValue;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");

            if(dragValue <= minimumDragDistance)
            {
                targetChecker.StartCoroutine(targetChecker.CheckTarget());
                launcher.Launch();

                dragMeter.fillAmount = 0f;
            }
        }
    }
}
