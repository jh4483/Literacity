using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBall : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 initBallPos;
    Rigidbody2D rb;
    CircleCollider2D collider;
    public bool isDragging;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        isDragging = false;
    }

    private void Update()
    {
        if(transform.position == (Vector3)initBallPos)
        {
            collider.enabled = true;

            rb.isKinematic = false;
            rb.velocity = Vector2.zero;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initBallPos = transform.position;

        collider.enabled = false;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        isDragging = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rb.isKinematic = false;
        collider.enabled = true;
        isDragging = false;
        
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
