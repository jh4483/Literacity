using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSysController : MonoBehaviour
{
    //Player values
    public Vector3 kazInitialPos;
    public Quaternion kazInitialRot;

    //Camera Controls
    public Cinemachine.CinemachineVirtualCamera gameCam;
    public Cinemachine.CinemachineVirtualCamera kazCam;

    public Animator animator;
    public int combo;
    public bool isAnimating;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        kazInitialPos = transform.position;
        kazInitialRot = Quaternion.Euler(0, 0, 0);
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
                SetCamera();
                yield return new WaitForSeconds(1.0f);

                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(2.8f);

                yield return new WaitForSeconds(0.5f);
                ResetAnim();
                ResetKaz();

                ResetCamera();
                yield return new WaitForSeconds(1.0f);

                isAnimating = false;

                break;

            case 2:
                SetCamera();
                yield return new WaitForSeconds(1.0f);

                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(3.7f);

                yield return new WaitForSeconds(0.5f);
                ResetAnim();
                ResetKaz();

                ResetCamera();
                yield return new WaitForSeconds(1.0f);

                isAnimating = false;

                break;

            case 3:
                SetCamera();
                yield return new WaitForSeconds(1.0f);

                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(3.7f);

                yield return new WaitForSeconds(0.5f);
                ResetAnim();
                ResetKaz();

                ResetCamera();
                yield return new WaitForSeconds(1.0f);

                isAnimating = false;

                break;

            case 4:
                SetCamera();
                yield return new WaitForSeconds(1.0f);
                
                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(3.3f);

                yield return new WaitForSeconds(0.5f);
                ResetAnim();
                ResetKaz();

                ResetCamera();
                yield return new WaitForSeconds(1.0f);

                isAnimating = false;

                break;

            case 5:
                SetCamera();
                yield return new WaitForSeconds(1.0f);
                
                animator.SetBool("isReady", true);
                yield return new WaitForSeconds(0.23f);

                animator.SetInteger("ComboVal", combo);
                yield return new WaitForSeconds(3.0f);

                yield return new WaitForSeconds(0.5f);
                ResetAnim();
                ResetKaz();

                ResetCamera();
                yield return new WaitForSeconds(1.0f);

                isAnimating = false;

                break;

            default:
                break;
        }
    }

    void ResetKaz()
    {
        transform.position = kazInitialPos;
        transform.rotation = kazInitialRot;
    }

    void ResetAnim()
    {
        animator.SetBool("isReady", false);
        animator.SetInteger("ComboVal", 0);
    }

    void SetCamera()
    {
        gameCam.gameObject.SetActive(false);
        kazCam.gameObject.SetActive(true);
    }

    void ResetCamera()
    {
        gameCam.gameObject.SetActive(true);
        kazCam.gameObject.SetActive(false);
    }
}
