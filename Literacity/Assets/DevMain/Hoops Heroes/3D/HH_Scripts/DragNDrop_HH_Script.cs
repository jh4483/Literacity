using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop_HH_Script : MonoBehaviour
{
    public Camera targetCamera;

    [Header("Mouse Stuff")]
    public float zySense;
    public float heightMultiplier;
    public Vector3 mPosInWorld;
    public Vector3 mDragPos;

    //misc variables for some math
    private Vector3 flyingVec;
    private float yValRef; 

    [Header("Clamping")]
    public float minClamp;
    public float maxClamp;
    public float newZPos;
    public float minClampY = -25;
    public float maxClampY = 1;
    BallObserver_HH_Script ballObserver;

    void Start()
    {
        ballObserver = FindObjectOfType<BallObserver_HH_Script>();
    }
    Vector3 GetObjectPos()
    {
        // Converts the World Position of the Object to Screen Position
        return targetCamera.WorldToScreenPoint(transform.position);
    }

    void OnMouseDown()
    {
        mPosInWorld = Input.mousePosition - GetObjectPos();

        flyingVec = Input.mousePosition - mPosInWorld;

        yValRef = flyingVec.y;
    }

    void OnMouseDrag()
    {

        flyingVec = Input.mousePosition - mPosInWorld;

        float displacementY = flyingVec.y - yValRef;

        //Backbox math which sets the starting position for the object and adds as the mouse is moved up to sync Z and Y
        zySense = Input.mousePosition.y / Screen.height;
        newZPos = (flyingVec.y + zySense)/heightMultiplier;

        float zDelta = (displacementY * zySense)/heightMultiplier;

        mDragPos = targetCamera.ScreenToWorldPoint(new Vector3(flyingVec.x, flyingVec.y, flyingVec.z + zDelta));
        mDragPos.z = Mathf.Clamp(mDragPos.z, minClamp, maxClamp);
        //mDragPos.y = Mathf.Clamp(mDragPos.y, minClampY, maxClampY);
        
        transform.position = mDragPos;

    }

    void OnMouseUp()
    {
        ballObserver.TransmitInfo();
    }
}