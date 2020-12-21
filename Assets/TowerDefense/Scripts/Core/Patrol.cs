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
    [SerializeField]
    float swichProbability = 0.2f;

    // The list of all patrol nodes to visit. -> all enemies random
    // if 1 enemy die -> delete it from this list
    [SerializeField]
    public List<NPC> patrolPoints;

    [SerializeField]
    public TeamRight teamRight;
    public TeamLeft teamLeft;

    [SerializeField]
    public NPC nPC;

    // Private variables for base behaviour
    NavMeshAgent navMeshAgent;
    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    bool patrolFoward;
    float waitTimer;


    void Start()
    {
        teamRight = FindObjectOfType<TeamRight>();
        teamLeft = FindObjectOfType<TeamLeft>();
        nPC = GetComponent<NPC>();

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to" + gameObject.name);
        }
        else
        {
            if (nPC.teamright)
            {
                patrolPoints = teamLeft.teamLeft;
            }
            else
            {
                patrolPoints = teamRight.teamRight;
            }

            if (patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = Random.Range(0, patrolPoints.Count - 1); ;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficent patrol point for basic patrolling behaviour.");
            }
        }
    }

    void Update()
    {
        //Moving and check if close to the destination
        if (travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;

            //If we are going to wait, then wait.
            if (waiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        //If we are waiting (to kill the enemy)
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer <= totalWaitTime)
            {
                waiting = false;
                ChangePatrolPoint();
                SetDestination();
            }
        }
    }
    private void SetDestination()
    {
        if (patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

    // Select a new patrol point in the available list
    // but also with a small probability allows for us to move forward or backward 
    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= swichProbability)
        {
            patrolFoward = !patrolFoward;
        }

        if (patrolFoward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            if (--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }
}
