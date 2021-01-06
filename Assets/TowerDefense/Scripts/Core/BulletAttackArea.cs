using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttackArea : MonoBehaviour
{
    public NPC nPC;
    private Patrol patrol;
    public List<Collider> container;
    public SphereCollider attackCollider;

    private void Start()
    {
        nPC = GetComponentInParent<NPC>();
        patrol = GetComponentInParent<Patrol>();
        container = new List<Collider>();
        attackCollider = GetComponent<SphereCollider>();
        attackCollider.radius = 8;
    }

    public IEnumerator Waiting()
    {
        yield return new WaitForSeconds(.5f);
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

    private void OnTriggerStay(Collider other)
    {
        if (nPC && other && (patrol.enabled == true) && (nPC.AttackType == 1))
        {
            if (other.tag == "NormalAttack" || other.tag == "BulletAttack")
            {
                var otherParent = other.transform.parent;
                if (otherParent.GetComponent<Patrol>() && !otherParent.GetComponent<Patrol>().isDead)
                {
                    if (!nPC.isTeamright && (otherParent.tag == "Right" || otherParent.tag == "HeroRight"))
                    {
                        container.Add(other.transform.parent.GetComponent<SphereCollider>());
                        if (!patrol.isDead)
                        {
                            patrol.navMeshAgent.isStopped = true;
                            patrol.animator.SetBool("running", false);
                            patrol.animator.SetBool("enemyMeet", true);
                            if (container[0])
                            {
                                nPC.Attack(container[0]);

                                if (nPC.MaxHp <= 0 || container[0].GetComponent<NPC>().MaxHp <= 0)
                                {
                                    container.RemoveAt(0);
                                    StartCoroutine(Waiting());
                                }
                            }
                            else
                            {
                                nPC.Attack(other.transform.parent.GetComponent<SphereCollider>());

                                if (nPC.MaxHp <= 0 || other.transform.parent.GetComponent<NPC>().MaxHp <= 0)
                                {
                                    StartCoroutine(Waiting());
                                }
                            }

                        }
                    }
                    else if (nPC.isTeamright && (otherParent.tag == "Left" || otherParent.tag == "HeroLeft"))
                    {
                        container.Add(other.transform.parent.GetComponent<SphereCollider>());
                        if (!patrol.isDead)
                        {
                            patrol.navMeshAgent.isStopped = true;
                            patrol.animator.SetBool("running", false);
                            patrol.animator.SetBool("enemyMeet", true);

                            if (container[0])
                            {
                                nPC.Attack(container[0]);

                                if (nPC.MaxHp <= 0 || container[0].GetComponent<NPC>().MaxHp <= 0)
                                {
                                    container.RemoveAt(0);
                                    StartCoroutine(Waiting());
                                }
                            }
                            else
                            {
                                nPC.Attack(other.transform.parent.GetComponent<SphereCollider>());

                                if (nPC.MaxHp <= 0 || other.transform.parent.GetComponent<NPC>().MaxHp <= 0)
                                {
                                    StartCoroutine(Waiting());
                                }
                            }
                        }
                    }
                }
            }
            if (nPC.isTeamright && other.tag == "TowerLeft")
            {
                //container.Add(other);
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
                            nPC.AttackTower(other);
                            if (nPC.MaxHp <= 0 || other.GetComponent<TeamLeft>().maxHP <= 0)
                            {
                                StartCoroutine(Waiting());
                            }
                        }
                    }

                }
            }
            else if (!nPC.isTeamright && other.tag == "TowerRight")
            {
                //container.Add(other);
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
                            nPC.AttackTower(other);
                            if (other && (nPC.MaxHp <= 0 || other.GetComponent<TeamRight>().maxHP <= 0))
                            {
                                StartCoroutine(Waiting());
                            }
                        }
                    }

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (patrol.enabled == true)
        {
            patrol.animator.SetBool("running", true);
            patrol.animator.SetBool("enemyMeet", false);
            StartCoroutine(Waiting());
        }
    }

}

