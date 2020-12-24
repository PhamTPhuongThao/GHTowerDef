using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject LevelText;
    public GameObject LevelTextCopy;
    public GameObject Canvas;

    public float maxAngleDownRight = 3 * Mathf.PI / 2;
    public float maxAngleUp = Mathf.PI / 2;
    public float maxAngleDown = -Mathf.PI / 2;

    private void Start()
    {
        patrol = FindObjectOfType<Patrol>();
        teamRight = FindObjectOfType<TeamRight>();
    }

    public void BuyMickey()
    {
        // if (Mickey.GetComponent<NPC>().isTeamright)
        // {
        //     maxAngleDown = maxAngleDownRight;
        // }

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
        LevelTextCopy = Instantiate(LevelText, spawnPos, launchPoint.rotation);
        LevelTextCopy.transform.SetParent(Canvas.transform, false);
        Debug.Log(LevelTextCopy.GetComponent<LevelText>().text);
        Mickey.GetComponent<NPC>().levelText = LevelTextCopy.GetComponent<LevelText>();
        Mickey.GetComponent<NPC>().Name = "Mickey";
        // Adding tag
        if (!Mickey.GetComponent<NPC>().isTeamright)
        {
            Mickey.gameObject.tag = "HeroLeft";
            Mickey.GetComponent<Patrol>().patrolPoint = teamRight.transform.position;
        }
        else
        {
            Mickey.gameObject.tag = "HeroRight";
            Mickey.GetComponent<Patrol>().patrolPoint = teamLeft.transform.position;
        }
    }

    public void BuyRalph()
    {
        // if (Ralph.GetComponent<NPC>().isTeamright)
        // {
        //     maxAngleDown = maxAngleDownRight;
        // }

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
        LevelTextCopy = Instantiate(LevelText, spawnPos, launchPoint.rotation);
        LevelTextCopy.transform.SetParent(Canvas.transform, false);
        Ralph.GetComponent<NPC>().levelText = LevelTextCopy.GetComponent<LevelText>();
        Ralph.GetComponent<NPC>().Name = "Ralph";

        // Adding tag
        if (!Ralph.GetComponent<NPC>().isTeamright)
        {
            Ralph.gameObject.tag = "HeroLeft";
            Ralph.GetComponent<Patrol>().patrolPoint = teamRight.transform.position;
        }
        else
        {
            Ralph.gameObject.tag = "HeroRight";
            Ralph.GetComponent<Patrol>().patrolPoint = teamLeft.transform.position;
        }
    }
}
