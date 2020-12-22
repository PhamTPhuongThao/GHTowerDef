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
    public Transform launchPoint;
    private GameObject hero;

    public void StartGame()
    {
        heroLoader = FindObjectOfType<HeroLoader>();

        var maxAngleUp = Mathf.PI / 2;
        var maxAngleDown = -Mathf.PI / 2;
        var angle = 0f;
        var pos = Vector3.zero;
        var spawnPos = Vector3.zero;
        if (heroLoader.heroesCollectionOfOurTeam.heroes.Length > 0)
        {
            for (int i = 0; i < heroLoader.heroesCollectionOfOurTeam.heroes.Length; i++)
            {
                if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Mickey")
                {
                    angle = Random.Range(maxAngleUp, maxAngleDown);
                    hero = Mickey;

                }
                else if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Ralph")
                {
                    angle = Random.Range(maxAngleUp, maxAngleDown);
                    hero = Ralph;
                }

                pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
                spawnPos = launchPoint.position + pos;
                Instantiate(hero, spawnPos, launchPoint.rotation);
                hero.GetComponent<NavMeshAgent>().speed = heroLoader.heroesCollectionOfOurTeam.heroes[i].MovementSpeed;
                hero.GetComponent<NPC>().MaxHp = heroLoader.heroesCollectionOfOurTeam.heroes[i].MaxHp;
                hero.GetComponent<NPC>().MaxAttack = heroLoader.heroesCollectionOfOurTeam.heroes[i].MaxAttack;
                hero.GetComponent<NPC>().AttackMiss = heroLoader.heroesCollectionOfOurTeam.heroes[i].AttackMiss;
                hero.GetComponent<NPC>().PhysicalDefense = heroLoader.heroesCollectionOfOurTeam.heroes[i].PhysicalDefense;
                hero.GetComponent<NPC>().CriticalChance = heroLoader.heroesCollectionOfOurTeam.heroes[i].CriticalChance;
                hero.GetComponent<NPC>().CriticalDamage = heroLoader.heroesCollectionOfOurTeam.heroes[i].CriticalDamage;
                hero.GetComponent<NPC>().AttackSpeed = heroLoader.heroesCollectionOfOurTeam.heroes[i].AttackSpeed;
                hero.GetComponent<NPC>().AttackType = heroLoader.heroesCollectionOfOurTeam.heroes[i].AttackType;
            }
        }

        if (heroLoader.soldier.NumberOfArmy > 0)
        {
            var numberOfSoldierMickey = Random.Range(1, heroLoader.soldier.NumberOfArmy);
            for (int i = 0; i < numberOfSoldierMickey; i++)
            {
                angle = Random.Range(maxAngleUp, maxAngleDown);
                hero = SoldierMickey;
                pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 10;
                spawnPos = launchPoint.position + pos;
                Instantiate(hero, spawnPos, launchPoint.rotation);
            }
            for (int i = 0; i < heroLoader.soldier.NumberOfArmy - numberOfSoldierMickey; i++)
            {
                angle = Random.Range(maxAngleUp, maxAngleDown);
                hero = SoldierRalph;
                pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 10;
                spawnPos = launchPoint.position + pos;
                Instantiate(hero, spawnPos, launchPoint.rotation);
            }

        }
    }
}
