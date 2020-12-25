using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamLeft : MonoBehaviour
{
    public int maxHP;
    public int value;
    public HeroLoader heroLoader;
    public GameObject winScreen;
    public GameObject loseScreen;
    public TeamRight teamRight;

    private void Start()
    {
        heroLoader = FindObjectOfType<HeroLoader>();
        teamRight = FindObjectOfType<TeamRight>();
        value = 200;
    }

    public void GetHurt(int amountBlood)
    {
        maxHP -= amountBlood;
        if (maxHP < 0)
        {
            maxHP = 0;
            KillEnemyCoinGetting();
            Destroy(this.gameObject);
            Destroy(teamRight.gameObject);
        }
    }

    public void KillEnemyCoinGetting()
    {
        if (heroLoader.chooseTeamLeft)
        {
            EnemyCoinSystem.GetCoin(value);
            loseScreen.SetActive(true);
        }
        else
        {
            CoinSystem.GetCoin(value);
            winScreen.SetActive(true);

        }
    }
}