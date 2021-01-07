﻿using System.Collections;
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
            if (currentEnemy == null)
            {
                AfterWar();
            }
            else
            {
                if (currentEnemy.transform.parent.GetComponent<SphereCollider>())
                {
                    nPC.Attack(currentEnemy.transform.parent.GetComponent<SphereCollider>());
                }
                else
                {
                    nPC.AttackTower(currentEnemy);
                }
            }
        }
    }


    private void AfterWar()
    {
        isAttacking = false;
        patrol.navMeshAgent.isStopped = false;
        patrol.animator.SetBool("running", true);
        patrol.animator.SetBool("enemyMeet", false);

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
                patrol.SetDestination(patrol.aim);
            }
        }
    }

    private void MeetEnemy(Collider other)
    {
        if (!patrol.isDead)
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
        if (!patrol.isDead)
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
                    patrol.animator.SetBool("running", false);
                    patrol.animator.SetBool("enemyMeet", true);
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
                if (otherParent.GetComponent<Patrol>() && !otherParent.GetComponent<Patrol>().isDead)
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
            isAttacking = false;
        }
    }

}
