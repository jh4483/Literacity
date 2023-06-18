using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragBasketball : MonoBehaviour
{
    [Header("Variables")]
    public bool isPressed = false;
    [SerializeField] LayerMask layer;
    Ray ray;
    RaycastHit hit;

    [Header("References")]
    public BasketballLauncher launcher;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);            //converts mouse position to screenspace

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layer))            //checks for a raycast hit
            {

                launcher.Launch();
                TargetCheck.CheckTarget();

                Debug.Log(hit.collider.name);
            }
        }
    }
}
