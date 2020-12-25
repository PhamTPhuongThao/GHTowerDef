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

    public GameObject Canvas;

    public void StartGame()
    {
        heroLoader = FindObjectOfType<HeroLoader>();
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

                Canvas.GetComponent<BuyingSystem>().BuyHero(hero, 0, true, heroLoader.heroesCollectionOfOurTeam.heroes[i]);
            }
        }

        if (heroLoader.soldier.NumberOfArmy > 0)
        {
            var numberOfSoldierMickey = Random.Range(1, heroLoader.soldier.NumberOfArmy);
            for (int i = 0; i < numberOfSoldierMickey; i++)
            {
                CreateArmy(SoldierMickey);
            }
            for (int i = 0; i < heroLoader.soldier.NumberOfArmy - numberOfSoldierMickey; i++)
            {
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

}
