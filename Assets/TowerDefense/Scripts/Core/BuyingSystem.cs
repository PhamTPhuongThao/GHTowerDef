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

    public NPCLevelText NPCLevelText;
    public NPCBloodBar NPCTeamBloodBar;
    public NPCBloodBar NPCEnemyBloodBar;
    public Canvas canvas;
    public HeroLoader HeroLoader;

    public float maxAngleDownRight = 3 * Mathf.PI / 2;
    public float maxAngleUp = Mathf.PI / 2;
    public float maxAngleDown = -Mathf.PI / 2;

    public bool isOurTeamRecover;
    public bool isEnemyTeamRecover;


    private void Start()
    {
        patrol = FindObjectOfType<Patrol>();
        teamRight = FindObjectOfType<TeamRight>();
        teamLeft = FindObjectOfType<TeamLeft>();
        isOurTeamRecover = true;
        isEnemyTeamRecover = true;
    }

    public void BuyMickey()
    {
        if (isOurTeamRecover)
        {
            isOurTeamRecover = false;
            BuyHero(Mickey, 50, false, true, null);
        }
    }

    public void BuyRalph()
    {
        if (isOurTeamRecover)
        {
            isOurTeamRecover = false;
            BuyHero(Ralph, 40, false, true, null);
        }
    }

    public IEnumerator WaitingOurTeamToRecover()
    {
        yield return new WaitForSeconds(5f);
        isOurTeamRecover = true;
    }

    public IEnumerator WaitingEnemyTeamToRecover()
    {
        yield return new WaitForSeconds(5f);
        isEnemyTeamRecover = true;
    }

    public void BuyHero(GameObject hero, int value, bool start, bool enemyOrOurTeam, HeroLoader.Hero heroClass)
    {
        if (CoinSystem.Instance.totalCoin < value)
        {
            return;
        }
        ChooseTeam(enemyOrOurTeam, hero);
        InstantiateHero(hero, value);
        SetConfig(hero, start, heroClass);
        CreateLabel(enemyOrOurTeam, hero);
        if (enemyOrOurTeam)
        {
            StartCoroutine(WaitingOurTeamToRecover());
        }
        else
        {
            StartCoroutine(WaitingEnemyTeamToRecover());
        }
    }

    public void ChooseTeam(bool enemyOrOurTeam, GameObject hero)
    {
        if (enemyOrOurTeam)
        {
            hero.GetComponent<NPC>().isTeamright = !HeroLoader.chooseTeamLeft;
            if (teamRight && teamLeft)
            {
                if (HeroLoader.GetComponent<HeroLoader>().chooseTeamLeft)
                {
                    maxAngleDown = -Mathf.PI / 2;
                    launchPoint = teamLeft.transform;
                    hero.gameObject.tag = "HeroLeft";
                }
                else
                {
                    maxAngleDown = maxAngleDownRight;
                    launchPoint = teamRight.transform;
                    hero.gameObject.tag = "HeroRight";
                }
            }
        }
        else // enemy
        {
            hero.GetComponent<NPC>().isTeamright = HeroLoader.chooseTeamLeft;
            if (teamRight && teamLeft)
            {
                if (HeroLoader.GetComponent<HeroLoader>().chooseTeamLeft)
                {
                    maxAngleDown = maxAngleDownRight;
                    launchPoint = teamRight.transform;
                    hero.gameObject.tag = "HeroRight";
                }
                else
                {
                    maxAngleDown = -Mathf.PI / 2;
                    launchPoint = teamLeft.transform;
                    hero.gameObject.tag = "HeroLeft";

                }
            }
        }
    }

    public void CreateLabel(bool enemyOrOurTeam, GameObject hero)
    {
        // var LevelTextCopy = NPCLevelText;
        // LevelTextCopy = Instantiate(NPCLevelText, spawnPos, launchPoint.rotation);
        // LevelTextCopy.transform.SetParent(canvas.transform, false);
        // hero.GetComponent<NPC>().NPCLevelText = LevelTextCopy.GetComponent<NPCLevelText>();
        var BloodBarCopy = NPCTeamBloodBar;
        if (enemyOrOurTeam)
        {
            BloodBarCopy = NPCTeamBloodBar;
        }
        else
        {
            BloodBarCopy = NPCEnemyBloodBar;
        }

        BloodBarCopy = Instantiate(BloodBarCopy, spawnPos, launchPoint.rotation);
        BloodBarCopy.transform.SetParent(canvas.transform, false);
        hero.GetComponent<NPC>().NPCBloodBar = BloodBarCopy.GetComponent<NPCBloodBar>();
    }

    public void InstantiateHero(GameObject hero, int value)
    {
        var angle = Random.Range(maxAngleUp, maxAngleDown);
        var pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
        spawnPos = launchPoint.position + pos;
        CoinSystem.Instance.SpendCoin(value);
        Instantiate(hero, spawnPos, launchPoint.rotation);
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
            if (heroClass != null)
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
        }
        else
        {
            // SET DEFAULT OF ITEM 

        }
    }

}
