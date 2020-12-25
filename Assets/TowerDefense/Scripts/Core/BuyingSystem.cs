using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyingSystem : MonoBehaviour
{
    //public int value;
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

    public GameObject HeroLoader;

    public LevelText levelTextContainer;

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
        BuyHero(Mickey, 50, false, null);
    }

    public void BuyRalph()
    {
        BuyHero(Ralph, 40, false, null);
    }

    public void BuyHero(GameObject hero, int value, bool start, HeroLoader.Hero heroClass)
    {
        var currNPC = hero.GetComponent<NPC>();
        levelTextContainer = currNPC.levelText;
        ChooseTeam();
        InstantiateHero(hero, value);
        SetConfig(hero, start, heroClass);
        AddingTag(hero);
    }

    public void ChooseTeam()
    {
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

    public void CreateLabelText(GameObject hero)
    {
        var currNPC = hero.GetComponent<NPC>();
        LevelTextCopy = Instantiate(LevelText, spawnPos, launchPoint.rotation);
        LevelTextCopy.transform.SetParent(Canvas.transform, false);
        levelTextContainer = LevelTextCopy.GetComponent<LevelText>();
        if (LevelTextCopy.GetComponent<LevelText>().text == null)
        {
            StartCoroutine(Waiting(LevelTextCopy));
        }
        currNPC.levelText = levelTextContainer;

    }

    public IEnumerator Waiting(GameObject LevelTextCopy)
    {
        yield return new WaitForSeconds(5f);
        levelTextContainer = LevelTextCopy.GetComponent<LevelText>();
    }

    public void InstantiateHero(GameObject hero, int value)
    {
        var angle = Random.Range(maxAngleUp, maxAngleDown);
        var pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
        spawnPos = launchPoint.position + pos;
        if (CoinSystem.totalCoin < value)
        {
            return;
        }
        CoinSystem.SpendCoin(value);
        Instantiate(hero, spawnPos, launchPoint.rotation);
    }

    public void AddingTag(GameObject hero)
    {
        var currNPC = hero.GetComponent<NPC>();
        currNPC.isTeamright = !HeroLoader.GetComponent<HeroLoader>().chooseTeamLeft;
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
        if (start)
        {
            hero.GetComponent<NavMeshAgent>().speed = heroClass.MovementSpeed;
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
        CreateLabelText(hero);

        if (hero == Mickey)
        {
            currNPC.Name = "Mickey";
        }
        else if (hero == Ralph)
        {
            currNPC.Name = "Ralph";
        }
    }

}
