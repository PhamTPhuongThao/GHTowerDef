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

    //public bool isDead;

    public GameObject remainEnemyHero;
    public GameObject remainEnemy;

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
    }

    void Update()
    {
        if (nPC.isDead)
        {
            return;
        }

        if (!teamRight)
        {
            nPC.countAttack = 0;
            if (nPC.NPCBloodBar)
            {
                Destroy(nPC.NPCBloodBar.gameObject);
            }
            Destroy(this.gameObject);
        }

        if (!teamLeft)
        {
            nPC.countAttack = 0;
            if (nPC.NPCBloodBar)
            {
                Destroy(nPC.NPCBloodBar.gameObject);
            }
            Destroy(this.gameObject);
        }

        if (this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "Left")
        {
            remainEnemyHero = GameObject.FindGameObjectWithTag("HeroRight");
            remainEnemy = GameObject.FindGameObjectWithTag("Right");
        }
        else if (this.gameObject.tag == "HeroRight" || this.gameObject.tag == "Right")
        {
            remainEnemyHero = GameObject.FindGameObjectWithTag("HeroLeft");
            remainEnemy = GameObject.FindGameObjectWithTag("Left");
        }
    }

    public void SetDestination(Vector3 setPatrolPoint)
    {
        animator.SetBool("running", true);
        animator.SetBool("enemyMeet", false);
        if (setPatrolPoint != null && !nPC.isDead)
        {
            Vector3 targetVector = setPatrolPoint;
            navMeshAgent.SetDestination(targetVector);
        }
    }

}

