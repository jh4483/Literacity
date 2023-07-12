using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBall : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }

    IEnumerator BallReset(float time)
    {
        yield return new WaitForSeconds(time);
        //ball reset position
    }

    IEnumerator BallDisappear(float time)
    {
        yield return new WaitForSeconds(time);
        //ball disappear w/VFX like sparkles
    }
}
