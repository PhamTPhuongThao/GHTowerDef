using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyingSystem : MonoBehaviour
{
    public GameObject Mickey;
    public GameObject Ralph;

    public Transform launchPoint;
    public Patrol patrol;
    public TeamRight teamRight;
    public TeamLeft teamLeft;
    public Vector3 spawnPos;

    public LevelText LevelText;
    public Canvas canvas;
    public HeroLoader HeroLoader;

    public float maxAngleDownRight = 3 * Mathf.PI / 2;
    public float maxAngleUp = Mathf.PI / 2;
    public float maxAngleDown = -Mathf.PI / 2;


    private void Start()
    {
        patrol = FindObjectOfType<Patrol>();
        teamRight = FindObjectOfType<TeamRight>();
        teamLeft = FindObjectOfType<TeamLeft>();
    }

    public void BuyMickey()
    {
        BuyHero(Mickey, 50, false, true, null);
    }

    public void BuyRalph()
    {
        BuyHero(Ralph, 40, false, true, null);
    }

    public void BuyHero(GameObject hero, int value, bool start, bool enemyOrOurTeam, HeroLoader.Hero heroClass)
    {
        ChooseTeam(enemyOrOurTeam, hero);
        InstantiateHero(hero, value);
        SetConfig(hero, start, heroClass);
        CreateLabelText(hero);
        AddingTag(hero);
    }

    public void ChooseTeam(bool enemyOrOurTeam, GameObject hero)
    {
        if (enemyOrOurTeam)
        {
            hero.GetComponent<NPC>().isTeamright = !HeroLoader.chooseTeamLeft;
            if (HeroLoader.GetComponent<HeroLoader>().chooseTeamLeft)
            {
                launchPoint = teamLeft.transform;
            }
            else
            {
                maxAngleDown = maxAngleDownRight;
                launchPoint = teamRight.transform;
            }
        }
        else // enemy
        {
            hero.GetComponent<NPC>().isTeamright = HeroLoader.chooseTeamLeft;
            if (HeroLoader.GetComponent<HeroLoader>().chooseTeamLeft)
            {
                maxAngleDown = maxAngleDownRight;
                launchPoint = teamRight.transform;
            }
            else
            {
                launchPoint = teamLeft.transform;
            }
        }
    }

    public void CreateLabelText(GameObject hero)
    {
        var LevelTextCopy = LevelText;
        LevelTextCopy = Instantiate(LevelText, spawnPos, launchPoint.rotation);
        LevelTextCopy.transform.SetParent(canvas.transform, false);
        hero.GetComponent<NPC>().levelText = LevelTextCopy.GetComponent<LevelText>();
    }

    public void InstantiateHero(GameObject hero, int value)
    {
        var angle = Random.Range(maxAngleUp, maxAngleDown);
        var pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
        spawnPos = launchPoint.position + pos;
        if (CoinSystem.Instance.totalCoin < value)
        {
            return;
        }
        CoinSystem.Instance.SpendCoin(value);
        Instantiate(hero, spawnPos, launchPoint.rotation);
    }

    public void AddingTag(GameObject hero)
    {
        var currNPC = hero.GetComponent<NPC>();
        //currNPC.isTeamright = !HeroLoader.GetComponent<HeroLoader>().chooseTeamLeft;
        if (!currNPC.isTeamright && teamRight)
        {
            hero.gameObject.tag = "HeroLeft";
            hero.GetComponent<Patrol>().patrolPoint = teamRight.transform.position;
        }
        else if (currNPC.isTeamright && teamLeft)
        {
            hero.gameObject.tag = "HeroRight";
            hero.GetComponent<Patrol>().patrolPoint = teamLeft.transform.position;
        }
    }

    public void SetConfig(GameObject hero, bool start, HeroLoader.Hero heroClass)
    {
        var currNPC = hero.GetComponent<NPC>();
        if (hero == Mickey)
        {
            currNPC.Name = "Mickey";
        }
        else if (hero == Ralph)
        {
            currNPC.Name = "Ralph";
        }
        if (start)
        {
            currNPC.MovementSpeed = heroClass.MovementSpeed;
            currNPC.Name = heroClass.Name;
            currNPC.MaxHp = heroClass.MaxHp;
            currNPC.MaxAttack = heroClass.MaxAttack;
            currNPC.AttackMiss = heroClass.AttackMiss;
            currNPC.PhysicalDefense = heroClass.PhysicalDefense;
            currNPC.CriticalChance = heroClass.CriticalChance;
            currNPC.CriticalDamage = heroClass.CriticalDamage;
            currNPC.AttackSpeed = heroClass.AttackSpeed;
            currNPC.AttackType = heroClass.AttackType;
        }
        else
        {
            // SET DEFAULT OF ITEM 

        }
    }

}
