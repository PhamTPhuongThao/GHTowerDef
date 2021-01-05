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

    private float maxAngleUp = Mathf.PI / 2;
    private float maxAngleDown = -Mathf.PI / 2;
    private float maxAngleDownRight = 3 * Mathf.PI / 2;
    private float angleLeft = 0f;
    private float angleRight = 0f;
    private Vector3 pos = Vector3.zero;
    private Vector3 spawnPos = Vector3.zero;

    public GameObject Canvas;

    BuyingSystem buyingSystem;
    private bool isReady;

    private void Start()
    {
        buyingSystem = Canvas.GetComponent<BuyingSystem>();
        heroLoader = FindObjectOfType<HeroLoader>();
        StartGame();
    }
    public void StartGame()
    {
        // StartCoroutine(CreateOurTeam());
        // StartCoroutine(CreateEnemyTeam());
        // StartCoroutine(StandStill());
    }
    private void Update()
    {

    }

    private IEnumerator CreateOurTeam()
    {
        if (heroLoader.heroesCollectionOfOurTeam.heroes.Length > 0)
        {
            for (int i = 0; i < heroLoader.heroesCollectionOfOurTeam.heroes.Length; i++)
            {
                if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Mickey")
                {
                    hero = Mickey;
                }
                else if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Ralph")
                {
                    hero = Ralph;
                }
                yield return new WaitForSeconds(.5f);
                buyingSystem.BuyHero(hero, 0, true, true, heroLoader.heroesCollectionOfOurTeam.heroes[i]);
            }
        }
    }

    private IEnumerator CreateEnemyTeam()
    {
        yield return new WaitForSeconds(1f);
        if (heroLoader.heroesCollectionOfEnemyTeam.heroes.Length > 0)
        {

            for (int i = 0; i < heroLoader.heroesCollectionOfEnemyTeam.heroes.Length; i++)
            {
                if (heroLoader.heroesCollectionOfEnemyTeam.heroes[i].Name == "Mickey")
                {
                    hero = Mickey;
                }
                else if (heroLoader.heroesCollectionOfEnemyTeam.heroes[i].Name == "Ralph")
                {
                    hero = Ralph;
                }
                yield return new WaitForSeconds(.5f);
                buyingSystem.BuyHero(hero, 0, true, false, heroLoader.heroesCollectionOfEnemyTeam.heroes[i]);
            }
        }
    }

    private IEnumerator CreateAllArmy()
    {
        if (heroLoader.soldier.NumberOfArmy > 0)
        {
            var numberOfSoldierMickey = Random.Range(1, heroLoader.soldier.NumberOfArmy);
            for (int i = 0; i < numberOfSoldierMickey; i++)
            {
                yield return new WaitForSeconds(1f);
                CreateArmy(SoldierMickey);
            }
            for (int i = 0; i < heroLoader.soldier.NumberOfArmy - numberOfSoldierMickey; i++)
            {
                yield return new WaitForSeconds(1f);
                CreateArmy(SoldierRalph);
            }
        }
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

    private IEnumerator EnemyBuyHero()
    {

        yield return new WaitForSeconds(2f);
        if (heroLoader.heroesCollectionOfEnemyTeam.numberOfHero > 0)
        {
            for (int i = 0; i < heroLoader.heroesCollectionOfEnemyTeam.numberOfHero; i++)
            {
                var r = Random.Range(1, 1000);
                if (r % 2 == 0)
                {
                    hero = Mickey;
                    yield return new WaitForSeconds(3f);
                    buyingSystem.BuyHero(hero, 0, true, false, null);
                }
                else
                {
                    hero = Ralph;
                    yield return new WaitForSeconds(3f);
                    buyingSystem.BuyHero(hero, 0, true, false, null);
                }

            }
        }
    }

    public IEnumerator StandStill()
    {
        yield return new WaitForSeconds(3f);
        if (buyingSystem.enemyTeamContainer.Count == heroLoader.heroesCollectionOfEnemyTeam.heroes.Length)
        {
            for (int i = 0; i < buyingSystem.ourTeamContainer.Count; i++)
            {
                buyingSystem.ourTeamContainer[i].GetComponent<Patrol>().enabled = true;
            }
            for (int i = 0; i < buyingSystem.enemyTeamContainer.Count; i++)
            {
                buyingSystem.enemyTeamContainer[i].GetComponent<Patrol>().enabled = true;
            }

        }
    }
}
