using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingSystem : MonoBehaviour
{
    public int value;
    public GameObject Mickey;
    public GameObject Ralph;
    public Transform launchPoint;
    public Patrol patrol;
    public TeamRight teamRight;

    private void Start()
    {
        patrol = FindObjectOfType<Patrol>();
        teamRight = FindObjectOfType<TeamRight>();
    }

    public void BuyMickey()
    {
        value = 50;
        if (CoinSystem.totalCoin < value)
        {
            return;
        }
        CoinSystem.SpendCoin(value);
        Instantiate(Mickey, launchPoint.position, launchPoint.rotation);
        if (teamRight.teamRight.Count == 0)
        {
            return;
        }
        Mickey.GetComponent<Patrol>().patrolPoints = teamRight.teamRight;
    }

    public void BuyRalph()
    {
        value = 40;
        if (CoinSystem.totalCoin < value)
        {
            return;
        }
        CoinSystem.SpendCoin(value);
        Instantiate(Ralph, launchPoint.position, launchPoint.rotation);
        if (teamRight.teamRight.Count == 0)
        {
            return;
        }
        Ralph.GetComponent<Patrol>().patrolPoints = teamRight.teamRight;
    }
}
