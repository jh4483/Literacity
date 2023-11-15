using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HH_DragNDrop : MonoBehaviour
{
    public Camera targetCamera;

    [Header("Mouse Stuff")]
    public float zySense;
    public float heightMultiplier;
    public Vector3 mPosInWorld;
    public Vector3 mDragPos;

    Vector3 GetObjectPos()
    {
        // Converts the World Position of the Object to Screen Position
        return targetCamera.WorldToScreenPoint(transform.position);
    }

    void OnMouseDown()
    {
        mPosInWorld = Input.mousePosition - GetObjectPos();
        //mPosInWorld.x = mPosInWorld.x / Screen.width;
        //mPosInWorld.y = mPosInWorld.y / Screen.height;
    }

    void OnMouseDrag()
    {

        Vector3 flyingVec = Input.mousePosition - mPosInWorld;

        //Backbox math which sets the starting position for the object and adds as the mouse is moved up to sync Z and Y
        zySense = Input.mousePosition.y / Screen.height;
        float newZPos = flyingVec.y * zySense/heightMultiplier;

        //float newZPos = flyingVec.z + flyingVec.y - (flyingVec.y * zySense);
        //float newZPos = flyingVec.z + flyingVec.y - zySense;

        mDragPos = targetCamera.ScreenToWorldPoint(new Vector3(flyingVec.x, flyingVec.y, newZPos));
        
        transform.position = mDragPos;
    }
}
