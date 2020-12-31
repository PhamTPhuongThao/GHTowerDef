using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public NPC nPC;
    private Patrol patrol;
    public Collider currentEnemy;

    private void Start()
    {
        nPC = GetComponentInParent<NPC>();
        patrol = GetComponentInParent<Patrol>();

    }

    public IEnumerator Waiting()
    {
        yield return new WaitForSeconds(.5f);
        patrol.navMeshAgent.isStopped = false;
        patrol.animator.SetBool("running", true);
        patrol.animator.SetBool("enemyMeet", false);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (nPC && other)
    //     {
    //         if (other.GetComponent<Patrol>() && !other.GetComponent<Patrol>().isDead)
    //         {
    //             if (!nPC.isTeamright && (other.tag == "Right" || other.tag == "HeroRight"))
    //             {
    //                 patrol.animator.SetBool("running", false);
    //                 patrol.animator.SetBool("enemyMeet", true);
    //             }
    //             else if (nPC.isTeamright && (other.tag == "Left" || other.tag == "HeroLeft"))
    //             {
    //                 patrol.animator.SetBool("running", false);
    //                 patrol.animator.SetBool("enemyMeet", true);
    //             }
    //         }
    //         if (nPC.isTeamright && other.tag == "TowerLeft")
    //         {
    //             patrol.animator.SetBool("running", false);
    //             patrol.animator.SetBool("enemyMeet", true);
    //         }
    //         else if (!nPC.isTeamright && other.tag == "TowerRight")
    //         {
    //             patrol.animator.SetBool("running", false);
    //             patrol.animator.SetBool("enemyMeet", true);
    //         }
    //     }
    // }

    private void OnTriggerStay(Collider other)
    {
        if (nPC && other)
        {
            if (other.GetComponent<Patrol>() && !other.GetComponent<Patrol>().isDead)
            {
                if (!nPC.isTeamright && (other.tag == "Right" || other.tag == "HeroRight"))
                {
                    if (!patrol.isDead)
                    {
                        patrol.navMeshAgent.isStopped = true;
                        patrol.animator.SetBool("running", false);
                        patrol.animator.SetBool("enemyMeet", true);
                        nPC.Attack(other);
                        if (nPC.MaxHp <= 0 || other.GetComponent<NPC>().MaxHp <= 0)
                        {
                            StartCoroutine(Waiting());
                        }
                    }

                }
                else if (nPC.isTeamright && (other.tag == "Left" || other.tag == "HeroLeft"))
                {
                    if (!patrol.isDead)
                    {

                        patrol.navMeshAgent.isStopped = true;
                        patrol.animator.SetBool("running", false);
                        patrol.animator.SetBool("enemyMeet", true);
                        nPC.Attack(other);
                        if (nPC.MaxHp <= 0 || other.GetComponent<NPC>().MaxHp <= 0)
                        {
                            StartCoroutine(Waiting());
                        }
                    }

                }
            }

            if (nPC.isTeamright && other.tag == "TowerLeft")
            {
                if (!patrol.isDead)
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
            else if (!nPC.isTeamright && other.tag == "TowerRight")
            {
                if (!patrol.isDead)
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

    private void OnTriggerExit(Collider other)
    {
        patrol.animator.SetBool("running", true);
        patrol.animator.SetBool("enemyMeet", false);
        StartCoroutine(Waiting());
    }

}
