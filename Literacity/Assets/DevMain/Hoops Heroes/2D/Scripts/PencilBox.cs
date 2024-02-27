using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilBox : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    public void OpenBox()
    {
        if(animator.GetBool("IntroDone"))
        {
            animator.SetBool("IntroDone", false);
        }
        else if(animator.GetBool("isOpen"))
        {
            animator.SetBool("isOpen", false);
        }
        else if(!animator.GetBool("isOpen"))
        {
            animator.SetBool("isOpen", true);
        }
    }
}
