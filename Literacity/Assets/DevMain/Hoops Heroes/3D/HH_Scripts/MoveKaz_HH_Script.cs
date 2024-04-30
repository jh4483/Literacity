using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class MoveKaz_HH_Script : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isMoving;

    private Ray ray;
    private RaycastHit hit;
    [SerializeField] private LayerMask COURTLAYER;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        HandlePlayerInput();
        if(isMoving)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void HandlePlayerInput()
    {
        if(Input.GetMouseButtonDown(1))
        {
            ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 1000f, COURTLAYER))
            {
                isMoving = true;
                agent.SetDestination(hit.point);
                Debug.Log(hit.point);
            }
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    isMoving = false;
                }
            }
        }
    }
}
