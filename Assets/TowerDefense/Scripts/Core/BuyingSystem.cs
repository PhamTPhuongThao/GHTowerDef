using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyingSystem : MonoBehaviour
{
    public int value;
    public GameObject Mickey;
    public GameObject Ralph;
    public Transform launchPoint;
    public Patrol patrol;
    public TeamRight teamRight;
    public TeamLeft teamLeft;

    public Vector3 spawnPos;

    private void Start()
    {
        patrol = FindObjectOfType<Patrol>();
        teamRight = FindObjectOfType<TeamRight>();

    }

    public void BuyMickey()
    {
        var maxAngleUp = Mathf.PI / 2;
        var maxAngleDown = -Mathf.PI / 2;
        if (Mickey.GetComponent<NPC>().isTeamright)
        {
            maxAngleDown = Mathf.PI / 2;
            maxAngleUp = -Mathf.PI / 2;
        }

        var angle = Random.Range(maxAngleUp, maxAngleDown);
        var pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
        spawnPos = launchPoint.position + pos;
        // Checking remain coin
        value = 50;
        if (CoinSystem.totalCoin < value)
        {
            return;
        }
        CoinSystem.SpendCoin(value);
        // Instantiate
        Instantiate(Mickey, spawnPos, launchPoint.rotation);
        // Checking whether have eneymy
        if (!Mickey.GetComponent<NPC>().isTeamright) // team left
        {
            Mickey.gameObject.tag = "HeroLeft";
            Mickey.GetComponent<Patrol>().patrolPoint = teamRight.transform.position;
        }
        else // team right
        {
            Mickey.gameObject.tag = "HeroRight";
            Mickey.GetComponent<Patrol>().patrolPoint = teamLeft.transform.position;
        }
    }

    public void BuyRalph()
    {
        var maxAngleUp = Mathf.PI / 2;
        var maxAngleDown = -Mathf.PI / 2;
        if (Ralph.GetComponent<NPC>().isTeamright)
        {
            maxAngleDown = Mathf.PI / 2;
            maxAngleUp = -Mathf.PI / 2;
        }
        var angle = Random.Range(maxAngleUp, maxAngleDown);
        var pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
        spawnPos = launchPoint.position + pos;
        // Checking remain coin
        value = 40;
        if (CoinSystem.totalCoin < value)
        {
            return;
        }
        CoinSystem.SpendCoin(value);
        // Instantiate
        Instantiate(Ralph, spawnPos, launchPoint.rotation);
        // Checking whether have eneymy
        if (!Ralph.GetComponent<NPC>().isTeamright) // team left
        {
            Ralph.gameObject.tag = "HeroLeft";
            Ralph.GetComponent<Patrol>().patrolPoint = teamRight.transform.position;
        }
        else // team right
        {
            Ralph.gameObject.tag = "HeroRight";
            Ralph.GetComponent<Patrol>().patrolPoint = teamLeft.transform.position;
        }
    }
}
