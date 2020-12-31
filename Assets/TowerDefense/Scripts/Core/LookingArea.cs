using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingArea : MonoBehaviour
{
    public NPC nPC;
    private Patrol patrol;
    public NPC enemyChosen;
    public float distanceToOther;


    private void Start()
    {
        nPC = GetComponentInParent<NPC>();
        patrol = GetComponentInParent<Patrol>();
        distanceToOther = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //     if (nPC && other)
        //     {
        //         if (other.GetComponent<Patrol>() && !other.GetComponent<Patrol>().isDead)
        //         {
        //             if (!nPC.isTeamright && (other.tag == "Right" || other.tag == "HeroRight"))
        //             {
        //                 patrol.patrolPoint = other.transform.position;
        //                 patrol.SetDestination(patrol.patrolPoint);
        //             }
        //             else if (nPC.isTeamright && (other.tag == "Left" || other.tag == "HeroLeft"))
        //             {
        //                 patrol.patrolPoint = other.transform.position;
        //                 patrol.SetDestination(patrol.patrolPoint);
        //             }
        //         }
        //         if (nPC.isTeamright && other.tag == "TowerLeft")
        //         {

        //         }
        //         else if (!nPC.isTeamright && other.tag == "TowerRight")
        //         {

        //         }
        //     }
        //     patrol.patrolPoint = patrol.aim;
        // }

        // private void OnTriggerStay(Collider other)
        // {
        //     if (nPC && other)
        //     {
        //         if (other.GetComponent<Patrol>() && !other.GetComponent<Patrol>().isDead)
        //         {
        //             if (!nPC.isTeamright && (other.tag == "Right" || other.tag == "HeroRight"))
        //             {
        //                 patrol.patrolPoint = other.transform.position;
        //                 patrol.SetDestination(patrol.patrolPoint);

        //             }
        //             else if (nPC.isTeamright && (other.tag == "Left" || other.tag == "HeroLeft"))
        //             {
        //                 patrol.patrolPoint = other.transform.position;
        //                 patrol.SetDestination(patrol.patrolPoint);

        //             }
        //         }

        //         if (nPC.isTeamright && other.tag == "TowerLeft")
        //         {
        //         }
        //         else if (!nPC.isTeamright && other.tag == "TowerRight")
        //         {
        //         }
        //     }
        //     patrol.patrolPoint = patrol.aim;
    }

}
