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
    BallBehaviour ballBehaviour;
    [SerializeField] float resetTime = 1.5f;

    private void Start()
    {
        StartCoroutine(BallDrop());
    }

    private void Update()
    {
        // if(transform.position == (Vector3)initBallPos)
        // {
        //     collider.enabled = true;

        //     rb.isKinematic = true;
        //     rb.velocity = Vector2.zero;
        //     rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // }
    }

    private IEnumerator BallDrop()
    {
        yield return new WaitForSeconds(3);
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        isDragging = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        ballBehaviour = GetComponent<BallBehaviour>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initBallPos = ballBehaviour.initialPos;

        collider.enabled = false;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        isDragging = true;
        rb.constraints = RigidbodyConstraints2D.None;
        StopAllCoroutines();

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
        StartCoroutine(BallReset(resetTime));
        
    }

    IEnumerator BallReset(float time)
    {
        yield return new WaitForSeconds(time);
        transform.position = ballBehaviour.initialPos;
        transform.rotation = ballBehaviour.initialRot;

        collider.enabled = true;

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    IEnumerator BallDisappear(float time)
    {
        yield return new WaitForSeconds(time);
        //ball disappear w/VFX like sparkles
    }


}
