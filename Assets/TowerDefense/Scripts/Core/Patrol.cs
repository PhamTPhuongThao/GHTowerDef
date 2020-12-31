using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    bool patrolWaiting;

    [SerializeField]
    public Vector3 patrolPoint;
    public Vector3 aim;

    [SerializeField]
    public TeamRight teamRight;
    public TeamLeft teamLeft;

    [SerializeField]
    public NPC nPC;
    public Animator animator;

    public NavMeshAgent navMeshAgent;

    public NPC currentEnemy;
    public bool isDead;

    void Start()
    {
        animator = GetComponent<Animator>();
        teamRight = FindObjectOfType<TeamRight>();
        teamLeft = FindObjectOfType<TeamLeft>();
        nPC = GetComponent<NPC>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = false;

        if (navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to" + gameObject.name);
        }
        else
        {
            if (nPC.isTeamright && teamLeft)
            {
                aim = teamLeft.transform.position;
            }
            else if (!nPC.isTeamright && teamRight)
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
        }

        currentEnemy = null;
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        patrolPoint = aim;

        // Right tower is destroyed
        if (!teamRight)
        {
            nPC.countAttack = 0;
            if (nPC.NPCBloodBar)
            {
                Destroy(nPC.NPCBloodBar.gameObject);
            }
            Destroy(this.gameObject);
        }

        // Left tower is destroyed
        if (!teamLeft)
        {
            nPC.countAttack = 0;
            if (nPC.NPCBloodBar)
            {
                Destroy(nPC.NPCBloodBar.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void SetDestination(Vector3 setPatrolPoint)
    {
        animator.SetBool("running", true);
        animator.SetBool("enemyMeet", false);
        if (patrolPoint != null && !isDead)
        {
            Vector3 targetVector = patrolPoint;
            navMeshAgent.SetDestination(targetVector);
        }
    }

}

