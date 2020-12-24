using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarManager : MonoBehaviour
{
    public HeroLoader heroLoader;
    public GameObject Mickey;
    public GameObject Ralph;
    public GameObject SoldierMickey;
    public GameObject SoldierRalph;
    public Transform launchPointLeft;
    public Transform launchPointRight;
    private GameObject hero;

    public float maxAngleUp = Mathf.PI / 2;
    public float maxAngleDown = -Mathf.PI / 2;
    public float maxAngleDownRight = 3 * Mathf.PI / 2;
    public float angle = 0f;
    public float angleLeft = 0f;
    public float angleRight = 0f;
    public Vector3 pos = Vector3.zero;
    public Vector3 spawnPos = Vector3.zero;

    public void StartGame()
    {
        // heroLoader = FindObjectOfType<HeroLoader>();
        // if (heroLoader.heroesCollectionOfOurTeam.heroes.Length > 0)
        // {
        //     for (int i = 0; i < heroLoader.heroesCollectionOfOurTeam.heroes.Length; i++)
        //     {
        //         if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Mickey")
        //         {
        //             angle = Random.Range(maxAngleUp, maxAngleDown);
        //             hero = Mickey;

        //         }
        //         else if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Ralph")
        //         {
        //             angle = Random.Range(maxAngleUp, maxAngleDown);
        //             hero = Ralph;
        //         }

        //         pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
        //         spawnPos = launchPointLeft.position + pos;
        //         Instantiate(hero, spawnPos, launchPointLeft.rotation);
        //         if (hero.GetComponent<NPC>().isTeamright)
        //         {
        //             hero.gameObject.tag = "HeroRight";
        //         }
        //         else
        //         {
        //             hero.gameObject.tag = "HeroLeft";
        //         }

        //         hero.GetComponent<NavMeshAgent>().speed = heroLoader.heroesCollectionOfOurTeam.heroes[i].MovementSpeed;
        //         hero.GetComponent<NPC>().MaxHp = heroLoader.heroesCollectionOfOurTeam.heroes[i].MaxHp;
        //         hero.GetComponent<NPC>().MaxAttack = heroLoader.heroesCollectionOfOurTeam.heroes[i].MaxAttack;
        //         hero.GetComponent<NPC>().AttackMiss = heroLoader.heroesCollectionOfOurTeam.heroes[i].AttackMiss;
        //         hero.GetComponent<NPC>().PhysicalDefense = heroLoader.heroesCollectionOfOurTeam.heroes[i].PhysicalDefense;
        //         hero.GetComponent<NPC>().CriticalChance = heroLoader.heroesCollectionOfOurTeam.heroes[i].CriticalChance;
        //         hero.GetComponent<NPC>().CriticalDamage = heroLoader.heroesCollectionOfOurTeam.heroes[i].CriticalDamage;
        //         hero.GetComponent<NPC>().AttackSpeed = heroLoader.heroesCollectionOfOurTeam.heroes[i].AttackSpeed;
        //         hero.GetComponent<NPC>().AttackType = heroLoader.heroesCollectionOfOurTeam.heroes[i].AttackType;
        //         hero.GetComponent<NPC>().level = 1;
        //     }
        // }

        // if (heroLoader.soldier.NumberOfArmy > 0)
        // {
        //     var numberOfSoldierMickey = Random.Range(1, heroLoader.soldier.NumberOfArmy);
        //     for (int i = 0; i < numberOfSoldierMickey; i++)
        //     {
        //         CreateArmy(SoldierMickey);
        //     }
        //     for (int i = 0; i < heroLoader.soldier.NumberOfArmy - numberOfSoldierMickey; i++)
        //     {
        //         CreateArmy(SoldierRalph);
        //     }

        // }
    }

    private void CreateArmy(GameObject heroType)
    {
        heroType.GetComponent<NPC>().isTeamright = false;
        heroType.gameObject.tag = "Left";
        angleLeft = Random.Range(maxAngleUp, maxAngleDown);
        pos = new Vector3(Mathf.Cos(angleLeft), 0, Mathf.Sin(angleLeft)) * 6;
        spawnPos = launchPointLeft.position + pos;
        Instantiate(heroType, spawnPos, launchPointLeft.rotation);

        heroType.GetComponent<NPC>().isTeamright = true;
        heroType.gameObject.tag = "Right";
        angleRight = Random.Range(maxAngleUp, maxAngleDownRight);
        pos = new Vector3(Mathf.Cos(angleRight), 0, Mathf.Sin(angleRight)) * 6;
        spawnPos = launchPointRight.position + pos;
        Instantiate(heroType, spawnPos, launchPointRight.rotation);
    }

}
