using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    public void OnSelected()
    {
        if(!animator.GetBool("isSelected"))
        {
            Debug.Log("called");
            animator.SetBool("isSelected", true);
            animator.SetBool("isDone", false);
        }
    }

    public void OnDone()
    {
        if(!animator.GetBool("isDone"))
        {
            animator.SetBool("isDone", true);
            animator.SetBool("isSelected", false);
        }
    }
}
