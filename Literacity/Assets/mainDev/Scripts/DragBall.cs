using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBall : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 initBallPos;
    Rigidbody2D rb;
    BoxCollider2D box;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(transform.position == (Vector3)initBallPos)
        {
            box.enabled = true;
            rb.isKinematic = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        initBallPos = transform.position;

        rb.velocity = Vector2.zero;

        box.enabled = false;
        rb.isKinematic = true;

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
