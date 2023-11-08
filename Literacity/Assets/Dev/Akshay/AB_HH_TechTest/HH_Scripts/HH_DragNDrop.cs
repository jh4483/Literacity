using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HH_DragNDrop : MonoBehaviour
{
    public Camera targetCamera;

    [Header("Mouse Stuff")]
    public Vector3 w2sPoint;        // World to Screen Point
    public Vector3 mPosInWorld;
    public Vector3 mDragPos;
    public Vector3 inMPos;

    Vector3 GetMousePos()
    {
        w2sPoint = targetCamera.WorldToScreenPoint(transform.position);

        return w2sPoint;
    }

    void OnMouseDown()
    {
        inMPos = Input.mousePosition;

        mPosInWorld = Input.mousePosition - GetMousePos();
    }

    void OnMouseDrag()
    {
        mDragPos = targetCamera.ScreenToWorldPoint(Input.mousePosition - mPosInWorld);
        
        transform.position = mDragPos;
    }




}
