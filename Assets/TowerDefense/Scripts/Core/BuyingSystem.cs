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

    public Vector3 spawnPos;

    private void Start()
    {
        patrol = FindObjectOfType<Patrol>();
        teamRight = FindObjectOfType<TeamRight>();


    }

    public void BuyMickey()
    {
        var angle = Random.Range(1, 100) * Mathf.PI * 2 / 100;
        var pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
        spawnPos = launchPoint.position + pos;
        value = 50;
        if (CoinSystem.totalCoin < value)
        {
            return;
        }
        CoinSystem.SpendCoin(value);
        Instantiate(Mickey, spawnPos, launchPoint.rotation);
        if (teamRight.teamRight.Count == 0)
        {
            return;
        }
        Mickey.GetComponent<Patrol>().patrolPoints = teamRight.teamRight;
    }

    public void BuyRalph()
    {
        var angle = Random.Range(1, 100) * Mathf.PI * 2 / 100;
        var pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
        spawnPos = launchPoint.position + pos;
        spawnPos = launchPoint.position;
        value = 40;
        if (CoinSystem.totalCoin < value)
        {
            return;
        }
        CoinSystem.SpendCoin(value);
        Instantiate(Ralph, spawnPos, launchPoint.rotation);
        if (teamRight.teamRight.Count == 0)
        {
            return;
        }
        Ralph.GetComponent<Patrol>().patrolPoints = teamRight.teamRight;
    }
}
