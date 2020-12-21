using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarManager : MonoBehaviour
{
    public HeroLoader heroLoader;
    public GameObject Mickey;
    public GameObject Ralph;
    public Transform launchPoint;


    public void StartGame()
    {
        heroLoader = FindObjectOfType<HeroLoader>();
        if (heroLoader.heroesCollectionOfOurTeam.heroes.Length > 0)
        {
            for (int i = 0; i < heroLoader.heroesCollectionOfOurTeam.heroes.Length; i++)
            {
                if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Mickey")
                {
                    Instantiate(Mickey, launchPoint.position, launchPoint.rotation);
                }
                else if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Ralph")
                {
                    Instantiate(Ralph, launchPoint.position, launchPoint.rotation);
                }
            }
        }

        // create army 
    }

}
