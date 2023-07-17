using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilBox : MonoBehaviour
{
    [SerializeField] private Vector2 closedPos;
    [SerializeField] private Vector2 openPos;
    [SerializeField] private float lidSpeed = 1f;
    [SerializeField] private bool isRunning = false;

    [SerializeField] private Animator animator;
    [SerializeField] private bool isClosed = true;

    public enum PencilBoxState
    {
        Closed,
        Open
    }

    [SerializeField] private PencilBoxState pencilBoxState;

    private void Start() 
    {
        animator = GetComponent<Animator>();

        closedPos = transform.localPosition;
    }

    public void OpenBox()
    {
        if(animator.GetBool("isOpen"))
        {
            animator.SetBool("isOpen", false);
        }
        else if(!animator.GetBool("isOpen"))
        {
            animator.SetBool("isOpen", true);
        }
    }




    void LidHandler()
    {
        switch (pencilBoxState)
        {
            case PencilBoxState.Closed:

                transform.localPosition = Vector2.Lerp(transform.localPosition, openPos, lidSpeed * Time.deltaTime);

                if((Vector2)transform.localPosition == openPos)
                {
                    pencilBoxState = PencilBoxState.Open;
                    isRunning = false;
                }

            break;

            case PencilBoxState.Open:

                transform.localPosition = Vector2.Lerp(transform.localPosition, closedPos, lidSpeed * Time.deltaTime);
                
                if((Vector2)transform.localPosition == closedPos)
                {
                    pencilBoxState = PencilBoxState.Closed;
                    isRunning = false;
                }

            break;
            
            default:
            break;
        }
    }
}
