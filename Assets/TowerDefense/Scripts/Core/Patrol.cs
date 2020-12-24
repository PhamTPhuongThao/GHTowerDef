using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    bool patrolWaiting;

    [SerializeField]
    float totalWaitTime = 2f;

    [SerializeField]
    public Vector3 patrolPoint;
    public Vector3 aim;

    [SerializeField]
    public TeamRight teamRight;
    public TeamLeft teamLeft;

    [SerializeField]
    public NPC nPC;
    public Animator animator;

    NavMeshAgent navMeshAgent;
    bool travelling;
    bool waiting;
    float waitTimer;

    public bool isDead;

    void Start()
    {
        animator = GetComponent<Animator>();
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
            if (nPC.isTeamright)
            {
                aim = teamLeft.transform.position;
            }
            else
            {
                aim = teamRight.transform.position;
            }
            patrolPoint = aim;
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
        animator.SetBool("running", true);
        animator.SetBool("enemyMeet", false);
        if (isDead)
        {
            return;
        }
        patrolPoint = aim;
        if (travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;
            waiting = true;
            waitTimer = 0f;
        }

        if (waiting)
        {
            animator.SetBool("running", false);
            animator.SetBool("enemyMeet", true);
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;
                SetDestination(patrolPoint);
            }
        }
    }

    private void SetDestination(Vector3 setPatrolPoint)
    {
        animator.SetBool("running", true);
        animator.SetBool("enemyMeet", false);
        if (patrolPoint != null && !isDead)
        {
            Vector3 targetVector = patrolPoint;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nPC && other)
        {
            if (!nPC.isTeamright && (other.tag == "Right" || other.tag == "HeroRight") && !other.GetComponent<Patrol>().isDead)
            {
                patrolPoint = other.transform.position;
                SetDestination(patrolPoint);
                nPC.Attack(other);
            }
            else if (nPC.isTeamright && (other.tag == "Left" || other.tag == "HeroLeft") && !other.GetComponent<Patrol>().isDead)
            {
                patrolPoint = other.transform.position;
                SetDestination(patrolPoint);
                nPC.Attack(other);
            }
            else if (nPC.isTeamright && other.tag == "TowerLeft")
            {
                nPC.AttackTower(other);
            }
            else if (!nPC.isTeamright && other.tag == "TowerRight")
            {
                nPC.AttackTower(other);
            }
        }
    }
}
