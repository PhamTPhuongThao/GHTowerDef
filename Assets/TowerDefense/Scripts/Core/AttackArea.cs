using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public NPC nPC;
    private Patrol patrol;
    public List<Collider> container;
    public SphereCollider attackCollider;
    public bool isAttacking;
    public Collider currentEnemy;
    public ParticleSystem RalphskillEffect;
    public ParticleSystem MickeyskillEffect;
    public ParticleSystem RalphAttack1;
    public ParticleSystem RalphAttack2;
    public ParticleSystem RalphAttack3;

    private void Start()
    {
        nPC = GetComponentInParent<NPC>();
        patrol = GetComponentInParent<Patrol>();
        container = new List<Collider>();
        attackCollider = GetComponent<SphereCollider>();
        attackCollider.radius = 2;
        isAttacking = false;
    }
    private void Update()
    {
        if (isAttacking)
        {
            patrol.animator.SetBool("running", false);
            patrol.animator.SetBool("enemyMeet", true);
            if (currentEnemy == null)
            {
                AfterWar();
            }
            else
            {
                if (currentEnemy.transform.parent.GetComponent<SphereCollider>())
                {
                    if (currentEnemy.GetComponent<NPC>() && currentEnemy.GetComponent<NPC>().isDead)
                    {
                        AfterWar();
                    }
                    nPC.Attack(currentEnemy.transform.parent.GetComponent<SphereCollider>());
                }
                else
                {
                    nPC.AttackTower(currentEnemy);
                }
            }
        }
        if (nPC.isDoingSkill)
        {
            if (nPC.Name == "Mickey" && nPC.AttackType == 0)
            {
                Instantiate(MickeyskillEffect, currentEnemy.transform.position, currentEnemy.transform.rotation);
                nPC.isDoingSkill = false;

            }
            if (nPC.Name == "Ralph" && nPC.AttackType == 0)
            {
                Instantiate(RalphskillEffect, currentEnemy.transform.position, currentEnemy.transform.rotation);
                nPC.isDoingSkill = false;
            }
        }

        // if (nPC.isAttacking && nPC.AttackType == 0 && nPC.Name == "Ralph")
        // {
        //     Instantiate(RalphAttack1, currentEnemy.transform.position, currentEnemy.transform.rotation);
        //     nPC.isAttacking = false;
        // }

    }

    private IEnumerator WaitingForDoingSkill()
    {
        yield return new WaitForSeconds(1f);
    }


    private void AfterWar()
    {
        isAttacking = false;
        patrol.navMeshAgent.isStopped = false;
        patrol.animator.SetBool("running", true);
        patrol.animator.SetBool("enemyMeet", false);
        patrol.SetDestination(patrol.aim);

        if (patrol.remainEnemyHero)
        {
            patrol.SetDestination(patrol.remainEnemyHero.transform.position);
            if (patrol.remainEnemy && patrol.remainEnemy.GetComponent<SphereCollider>())
            {
                currentEnemy = patrol.remainEnemy.GetComponent<SphereCollider>();
            }
            else
            {
                currentEnemy = null;
            }
        }
        else
        {
            if (patrol.remainEnemy)
            {
                patrol.SetDestination(patrol.remainEnemy.transform.position);
                if (patrol.remainEnemy.GetComponent<SphereCollider>())
                {
                    currentEnemy = patrol.remainEnemy.GetComponent<SphereCollider>();
                }
                else
                {
                    currentEnemy = null;
                }
            }
            else
            {
                patrol.SetDestination(patrol.aim);
            }
        }
    }

    private void MeetEnemy(Collider other)
    {
        if (!nPC.isDead)
        {
            patrol.navMeshAgent.isStopped = true;
            patrol.animator.SetBool("running", false);
            patrol.animator.SetBool("enemyMeet", true);
            isAttacking = true;
            currentEnemy = other;
        }
    }

    private void MeetTower(Collider other)
    {
        if (!nPC.isDead)
        {
            if (patrol.remainEnemyHero)
            {
                patrol.SetDestination(patrol.remainEnemyHero.transform.position);
            }
            else
            {
                if (patrol.remainEnemy)
                {
                    patrol.SetDestination(patrol.remainEnemy.transform.position);
                }
                else
                {
                    patrol.navMeshAgent.isStopped = true;
                    patrol.animator.SetBool("enemyMeet", true);
                    patrol.animator.SetBool("running", false);
                    isAttacking = true;
                    currentEnemy = other;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (nPC && other && (patrol.enabled == true) && (nPC.AttackType == 0))
        {
            if (other.tag == "NormalAttack")
            {
                var otherParent = other.transform.parent;
                if (otherParent.GetComponent<Patrol>() && !otherParent.GetComponent<NPC>().isDead)
                {
                    if (!nPC.isTeamright && (otherParent.tag == "Right" || otherParent.tag == "HeroRight"))
                    {
                        MeetEnemy(other);
                    }
                    else if (nPC.isTeamright && (otherParent.tag == "Left" || otherParent.tag == "HeroLeft"))
                    {
                        MeetEnemy(other);
                    }
                }
            }

            if (nPC.isTeamright && other.tag == "TowerLeft")
            {
                MeetTower(other);
            }
            else if (!nPC.isTeamright && other.tag == "TowerRight")
            {
                MeetTower(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (patrol.enabled == true)
        {
            patrol.animator.SetBool("running", true);
            patrol.animator.SetBool("enemyMeet", false);
            //isAttacking = false;
        }
    }

}
