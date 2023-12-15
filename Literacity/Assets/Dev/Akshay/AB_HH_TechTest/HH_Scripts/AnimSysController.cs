using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSysController : MonoBehaviour
{
    //Player values
    public Vector3 kazInitialPos;

    public Animator animator;
    public int combo;
    public bool isAnimating;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        kazInitialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAnimating)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                combo = 1;
                isAnimating = true;

                StartCoroutine(PlayAnimation());
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                combo = 2;
                isAnimating = true;

                StartCoroutine(PlayAnimation());
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                combo = 3;
                isAnimating = true;

                StartCoroutine(PlayAnimation());
            }
            if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                combo = 4;
                isAnimating = true;

                StartCoroutine(PlayAnimation());
            }
            if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                combo = 5;
                isAnimating = true;

                StartCoroutine(PlayAnimation());
            }
        }
    }

    public IEnumerator PlayAnimation()
    {
        switch(combo)
        {
            case 1:
                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(2.8f);

                transform.position = kazInitialPos;
                animator.SetBool("isReady", false);
                animator.SetInteger("ComboVal", 0);

                yield return new WaitForSeconds(0.5f);
                isAnimating = false;

                break;
            case 2:
                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(3.7f);

                transform.position = kazInitialPos;
                animator.SetBool("isReady", false);
                animator.SetInteger("ComboVal", 0);

                yield return new WaitForSeconds(0.5f);
                isAnimating = false;

                break;
            case 3:
                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(3.7f);

                transform.position = kazInitialPos;
                animator.SetBool("isReady", false);
                animator.SetInteger("ComboVal", 0);

                yield return new WaitForSeconds(0.5f);
                isAnimating = false;

                break;
            case 4:
                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(3.3f);

                transform.position = kazInitialPos;
                animator.SetBool("isReady", false);
                animator.SetInteger("ComboVal", 0);

                yield return new WaitForSeconds(0.5f);
                isAnimating = false;

                break;
            case 5:
                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(3.0f);

                transform.position = kazInitialPos;
                animator.SetBool("isReady", false);
                animator.SetInteger("ComboVal", 0);

                yield return new WaitForSeconds(0.5f);
                isAnimating = false;

                break;
            default:
                break;
        }
    }

    void ResetData()
    {
        transform.position = kazInitialPos;

        animator.SetBool("isReady", false);
    }
}
