using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreation : MonoBehaviour
{
    public HeroLoader heroLoader;
    public GameObject Mickey;
    public GameObject Ralph;
    public Transform launchPointLeft;
    public Transform launchPointRight;

    public GameObject Canvas;
    BuyingSystem buyingSystem;

    private void Start()
    {
        buyingSystem = Canvas.GetComponent<BuyingSystem>();
        heroLoader = FindObjectOfType<HeroLoader>();
        StartGame();
    }

    public void StartGame()
    {
        //StartCoroutine(EnemyBuyHero());
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
                    var hero = Mickey;
                    yield return new WaitForSeconds(4f);
                    if (launchPointLeft && launchPointRight)
                    {
                        buyingSystem.BuyHero(hero, 0, false, false, null);
                    }
                }
                else
                {
                    var hero = Ralph;
                    yield return new WaitForSeconds(4f);
                    if (launchPointLeft && launchPointRight)
                    {
                        buyingSystem.BuyHero(hero, 0, false, false, null);
                    }
                }
            }
        }
    }

    // private IEnumerator CreateEnemyTeam()
    // {

    //     if (heroLoader.heroesCollectionOfEnemyTeam.heroes.Length > 0)
    //     {
    //         for (int i = 0; i < heroLoader.heroesCollectionOfEnemyTeam.heroes.Length; i++)
    //         {
    //             if (heroLoader.heroesCollectionOfEnemyTeam.heroes[i].Name == "Mickey")
    //             {
    //                 hero = Mickey;
    //             }
    //             else if (heroLoader.heroesCollectionOfEnemyTeam.heroes[i].Name == "Ralph")
    //             {
    //                 hero = Ralph;
    //             }
    //             yield return new WaitForSeconds(.5f);
    //             buyingSystem.BuyHero(hero, 0, true, false, heroLoader.heroesCollectionOfEnemyTeam.heroes[i]);
    //         }
    //     }
    // }

    // public IEnumerator StandStill()
    // {
    //     yield return new WaitForSeconds(3f);
    //     for (int i = 0; i < buyingSystem.ourTeamContainer.Count; i++)
    //     {
    //         buyingSystem.ourTeamContainer[i].GetComponent<Patrol>().enabled = true;
    //     }
    //     for (int i = 0; i < buyingSystem.enemyTeamContainer.Count; i++)
    //     {
    //         buyingSystem.enemyTeamContainer[i].GetComponent<Patrol>().enabled = true;
    //     }
    // }
}
