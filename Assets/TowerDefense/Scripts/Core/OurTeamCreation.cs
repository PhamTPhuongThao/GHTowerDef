using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurTeamCreation : MonoBehaviour
{
    public HeroLoader heroLoader;
    public GameObject Mickey;
    public GameObject Ralph;
    public Transform launchPointLeft;
    public Transform launchPointRight;
    private GameObject hero;


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
        StartCoroutine(CreateOurTeam());
        StartCoroutine(StandStill());
    }

    private IEnumerator CreateOurTeam()
    {
        yield return new WaitForSeconds(1f);
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

    public IEnumerator StandStill()
    {
        yield return new WaitForSeconds(3f);
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
