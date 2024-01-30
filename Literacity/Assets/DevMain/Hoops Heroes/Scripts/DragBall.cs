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

    public BoosterState boosterState;
    public SpreadSheetNew spreadSheetNew;

    private void Start()
    {
        StartCoroutine(BallDrop());
        gameMask = GameObject.Find("Canvas").transform.Find("Mask").gameObject;

        boosterState = FindObjectOfType<BoosterState>();
        spreadSheetNew = FindObjectOfType<SpreadSheetNew>();
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

        yield return new WaitForSeconds(8);

        initBallPos = GetComponent<RectTransform>().anchoredPosition;
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
        if(boosterState.AnimIsRunning)
        {
            return;
        }
        if(spreadSheetNew.playStarted)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        rb.isKinematic = false;
        collider.enabled = true;
        isDragging = false;
        rb.gravityScale = 40;

        StartCoroutine(BallReset()); 
       
    }

    IEnumerator BallReset()
    {
        yield return new WaitForSeconds(3);
        GetComponent<RectTransform>().anchoredPosition = initBallPos;
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