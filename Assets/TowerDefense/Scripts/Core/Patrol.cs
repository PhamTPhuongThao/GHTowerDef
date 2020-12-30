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
    public bool inWar;

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
            else
            {
                Debug.Log("Insufficent patrol point for basic patrolling behaviour.");
            }
        }
    }

    void Update()
    {
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

        if (!teamRight && nPC.isTeamright)
        {
            nPC.countAttack = 0;
            // Destroy(nPC.NPCLevelText.gameObject);
            if (nPC.NPCBloodBar)
            {
                Destroy(nPC.NPCBloodBar.gameObject);
            }
            Destroy(this.gameObject);
        }
        if (!teamLeft && !nPC.isTeamright)
        {
            nPC.countAttack = 0;
            // Destroy(nPC.NPCLevelText.gameObject);
            if (nPC.NPCBloodBar)
            {
                Destroy(nPC.NPCBloodBar.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void SetDestination(Vector3 setPatrolPoint)
    {
        animator.SetBool("running", true);
        animator.SetBool("enemyMeet", false);
        if (patrolPoint != null && !isDead && navMeshAgent)
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
            if (other.GetComponent<Patrol>() && !other.GetComponent<Patrol>().isDead)
            {
                if (!nPC.isTeamright && (other.tag == "Right" || other.tag == "HeroRight") && !inWar)
                {
                    inWar = true;
                    patrolPoint = other.transform.position;
                    SetDestination(patrolPoint);
                    animator.SetBool("running", false);
                    animator.SetBool("enemyMeet", true);
                    nPC.Attack(other);
                }
                else if (nPC.isTeamright && (other.tag == "Left" || other.tag == "HeroLeft") && !inWar)
                {
                    inWar = true;
                    patrolPoint = other.transform.position;
                    SetDestination(patrolPoint);
                    animator.SetBool("running", false);
                    animator.SetBool("enemyMeet", true);
                    nPC.Attack(other);
                }
            }


            if (nPC.isTeamright && other.tag == "TowerLeft")
            {
                animator.SetBool("running", false);
                animator.SetBool("enemyMeet", true);
                nPC.AttackTower(other);
            }
            else if (!nPC.isTeamright && other.tag == "TowerRight")
            {
                animator.SetBool("running", false);
                animator.SetBool("enemyMeet", true);
                nPC.AttackTower(other);
            }
        }
        patrolPoint = aim;
        inWar = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (nPC && other)
        {
            if (other.GetComponent<Patrol>() && !other.GetComponent<Patrol>().isDead)
            {
                if (!nPC.isTeamright && (other.tag == "Right" || other.tag == "HeroRight") && !inWar)
                {
                    inWar = true;
                    patrolPoint = other.transform.position;
                    SetDestination(patrolPoint);
                    animator.SetBool("running", false);
                    animator.SetBool("enemyMeet", true);
                    nPC.Attack(other);
                }
                else if (nPC.isTeamright && (other.tag == "Left" || other.tag == "HeroLeft") && !inWar)
                {
                    inWar = true;
                    patrolPoint = other.transform.position;
                    SetDestination(patrolPoint);
                    animator.SetBool("running", false);
                    animator.SetBool("enemyMeet", true);
                    nPC.Attack(other);
                }
            }


            if (nPC.isTeamright && other.tag == "TowerLeft")
            {
                animator.SetBool("running", false);
                animator.SetBool("enemyMeet", true);
                nPC.AttackTower(other);
            }
            else if (!nPC.isTeamright && other.tag == "TowerRight")
            {

                animator.SetBool("running", false);
                animator.SetBool("enemyMeet", true);
                nPC.AttackTower(other);
            }
        }
        patrolPoint = aim;
        inWar = false;
    }

}

