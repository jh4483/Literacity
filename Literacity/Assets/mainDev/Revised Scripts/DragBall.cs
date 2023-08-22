using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBall : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject gameMask;
    Vector2 initBallPos;
    Rigidbody2D rb;
    CircleCollider2D collider;
    BallBehaviour ballBehaviour;
    public bool isDragging;

    private void Start()
    {
        StartCoroutine(BallDrop());
        gameMask = GameObject.Find("Canvas").transform.Find("Mask").gameObject;
    }

    private void Update()
    {

    }

    private IEnumerator BallDrop()
    {
        collider = GetComponent<CircleCollider2D>();
        yield return new WaitForSeconds(3);
        rb = GetComponent<Rigidbody2D>();
        isDragging = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        ballBehaviour = GetComponent<BallBehaviour>();
        initBallPos = transform.position;

        yield return new WaitForSeconds(8);
        gameMask.SetActive(false);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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

        StartCoroutine(BallReset()); 
       
    }

    IEnumerator BallReset()
    {
        yield return new WaitForSeconds(3);
        transform.position = initBallPos;
        transform.rotation = ballBehaviour.initialRot;

        collider.enabled = true;

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    IEnumerator BallDisappear(float time)
    {
        yield return new WaitForSeconds(time);
    }

}