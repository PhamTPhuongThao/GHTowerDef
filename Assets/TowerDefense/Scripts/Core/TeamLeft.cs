﻿using UnityEngine;

public class TeamLeft : MonoBehaviour
{
    public int maxHP;
    public int HPContainer;
    private int value;
    public HeroLoader heroLoader;
    public GameObject winScreen;
    public GameObject loseScreen;
    public TeamRight teamRight;
    public NPCBloodBar teamLeftHealthBar;
    public NPCBloodBar NPCHealthBar;
    public NPCBloodBar NPCEnemyHealthBar;
    public GameObject getHitParticle;
    public ParticleSystem smallFire;


    private void Start()
    {
        heroLoader = FindObjectOfType<HeroLoader>();
        teamRight = FindObjectOfType<TeamRight>();
        value = 200;
        maxHP = 15000;
        HPContainer = maxHP;
        teamLeftHealthBar.bloodBar.maxValue = maxHP;
    }
    private void Update()
    {
        teamLeftHealthBar.bloodBar.value = maxHP;

    }

    public void GetHurt(int amountBlood)
    {
        Instantiate(getHitParticle, transform.position + Vector3.up * 3, transform.rotation);
        maxHP -= amountBlood;
        if (maxHP < HPContainer * 0.7)
        {
            Instantiate(smallFire, transform.position + Vector3.left * 7, transform.rotation);
        }
        // if (maxHP < HPContainer * 0.3){
        //     Instantiate 
        // }
        if (maxHP < 0)
        {
            maxHP = 0;
            KillEnemyCoinGetting();
            // health bar of 2 team
            if (teamLeftHealthBar && teamRight.teamRightHealthBar)
            {
                Destroy(teamLeftHealthBar.gameObject);
                Destroy(teamRight.teamRightHealthBar.gameObject);
            }
            // health bar of NPC of 2 team
            if (NPCEnemyHealthBar && NPCEnemyHealthBar)
            {
                Destroy(NPCHealthBar.gameObject);
                Destroy(NPCEnemyHealthBar.gameObject);
            }
            // 2 team
            Destroy(this.gameObject);
            Destroy(teamRight.gameObject);

        }
    }

    public void KillEnemyCoinGetting()
    {
        if (heroLoader.chooseTeamLeft)
        {
            EnemyCoinSystem.Instance.GetCoin(value);
            loseScreen.SetActive(true);
        }
        else
        {
            CoinSystem.Instance.GetCoin(value);
            winScreen.SetActive(true);

        }
    }
}