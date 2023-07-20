using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start() 
    {
        //animator = GetComponent<Animator>();
    }

    public void OnSelected()
    {
        if(!animator.GetBool("isSelected"))
        {
            animator.SetBool("isSelected", true);
        }
    }

    public void OnDone()
    {
        if(!animator.GetBool("isDone"))
        {
            animator.SetBool("isDone", true);
        }
    }
}
