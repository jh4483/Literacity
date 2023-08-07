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
    private GameObject backBoardCollider;

    private void Start()
    {
        StartCoroutine(BallDrop());
        gameMask = GameObject.Find("Canvas").transform.Find("Mask").gameObject;
        backBoardCollider = GameObject.Find("Canvas").transform.Find("Backboard Highlight").gameObject;
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
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        ballBehaviour = GetComponent<BallBehaviour>();
        initBallPos = transform.position;
        gameMask.SetActive(false);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Backboard Highlight")
        {            
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 25f);        
        }
    }

    IEnumerator BallReset()
    {
        backBoardCollider.GetComponent<BoxCollider2D>().enabled = true;
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
