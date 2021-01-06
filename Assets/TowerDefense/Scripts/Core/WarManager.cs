using System.Collections;
using UnityEngine;

public class WarManager : MonoBehaviour
{
    public HeroLoader heroLoader;
    public GameObject SoldierMickey;
    public GameObject SoldierRalph;
    public NPCBloodBar teamHealthBar;
    public NPCBloodBar enemyHealthBar;
    public TeamLeft teamLeft;
    public TeamRight teamRight;

    public GameObject Canvas;
    BuyingSystem buyingSystem;

    // private float maxAngleUp = Mathf.PI / 2;
    // private float maxAngleDown = -Mathf.PI / 2;
    // private float maxAngleDownRight = 3 * Mathf.PI / 2;
    // private float angleLeft = 0f;
    // private float angleRight = 0f;
    // private Vector3 pos = Vector3.zero;
    // private Vector3 spawnPos = Vector3.zero;

    private void Start()
    {
        buyingSystem = Canvas.GetComponent<BuyingSystem>();
        heroLoader = FindObjectOfType<HeroLoader>();
        if (heroLoader.chooseTeamLeft)
        {
            teamLeft.teamLeftHealthBar = teamHealthBar;
            teamRight.teamRightHealthBar = enemyHealthBar;
        }
        else
        {
            teamLeft.teamLeftHealthBar = enemyHealthBar;
            teamRight.teamRightHealthBar = teamHealthBar;
        }

    }

    // private IEnumerator CreateAllArmy()
    // {
    //     if (heroLoader.soldier.NumberOfArmy > 0)
    //     {
    //         var numberOfSoldierMickey = Random.Range(1, heroLoader.soldier.NumberOfArmy);
    //         for (int i = 0; i < numberOfSoldierMickey; i++)
    //         {
    //             yield return new WaitForSeconds(1f);
    //             CreateArmy(SoldierMickey);
    //         }
    //         for (int i = 0; i < heroLoader.soldier.NumberOfArmy - numberOfSoldierMickey; i++)
    //         {
    //             yield return new WaitForSeconds(1f);
    //             CreateArmy(SoldierRalph);
    //         }
    //     }
    // }

    // private void CreateArmy(GameObject heroType)
    // {
    //     heroType.GetComponent<NPC>().isTeamright = false;
    //     heroType.gameObject.tag = "Left";
    //     angleLeft = Random.Range(maxAngleUp, maxAngleDown);
    //     pos = new Vector3(Mathf.Cos(angleLeft), 0, Mathf.Sin(angleLeft)) * 6;
    //     spawnPos = teamLeft.transform.position + pos;
    //     Instantiate(heroType, spawnPos, teamLeft.transform.rotation);

    //     heroType.GetComponent<NPC>().isTeamright = true;
    //     heroType.gameObject.tag = "Right";
    //     angleRight = Random.Range(maxAngleUp, maxAngleDownRight);
    //     pos = new Vector3(Mathf.Cos(angleRight), 0, Mathf.Sin(angleRight)) * 6;
    //     spawnPos = teamRight.transform.position + pos;
    //     Instantiate(heroType, spawnPos, teamRight.transform.rotation);
    // }
}
