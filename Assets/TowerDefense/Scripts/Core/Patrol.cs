using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    // Dictates whether the agent waits on each node
    // -> when fight -> true
    [SerializeField]
    bool patrolWaiting;

    //Total time wait at each node ->  (bool) if enemy die -> move to another patrolpoint
    [SerializeField]
    float totalWaitTime = 3f;

    //The probanility of swiching direction
    // [SerializeField]
    // float swichProbability = 0.2f;

    // The list of all patrol nodes to visit. -> all enemies random
    // if 1 enemy die -> delete it from this list
    [SerializeField]
    public Vector3 patrolPoint;

    [SerializeField]
    public TeamRight teamRight;
    public TeamLeft teamLeft;

    [SerializeField]
    public NPC nPC;
    public Animator animator;

    // Private variables for base behaviour
    NavMeshAgent navMeshAgent;

    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    // bool patrolFoward;
    float waitTimer;

    void Start()
    {
        animator = GetComponent<Animator>();
        teamRight = FindObjectOfType<TeamRight>();
        teamLeft = FindObjectOfType<TeamLeft>();
        nPC = GetComponent<NPC>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        patrolWaiting = true;

        if (navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to" + gameObject.name);
        }
        else
        {
            if (nPC.isTeamright)
            {
                patrolPoint = teamLeft.transform.position;
            }
            else
            {
                patrolPoint = teamRight.transform.position;
            }

            if (patrolPoint != null)
            {
                animator.SetInteger("attackOne", nPC.AttackType);
                navMeshAgent.speed = nPC.MovementSpeed;
                SetDestination(patrolPoint);
            }
            else
            {
                Debug.Log("Insufficent patrol point for basic patrolling behaviour.");
            }
        }
    }

    void Update()
    {
        if (travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;
            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                SetDestination(patrolPoint);
            }
        }
        if (waiting)
        {
            animator.SetBool("running", false);
            animator.SetBool("enemyMeet", true);
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;
                animator.SetBool("enemyMeet", false);
                SetDestination(patrolPoint);
            }
        }
    }
    private void SetDestination(Vector3 setPatrolPoint)
    {
        animator.SetBool("running", true);
        if (patrolPoint != null)
        {
            Vector3 targetVector = patrolPoint;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NPC>() == null)
        {
            return;
        }
        if (nPC != null)
        {
            if (!nPC.isTeamright && other.tag == "Right")
            {
                patrolPoint = other.transform.position;
                SetDestination(patrolPoint);
            }
            else if (nPC.isTeamright && other.tag == "Left")
            {
                patrolPoint = other.transform.position;
                SetDestination(patrolPoint);
            }
        }
    }
}
