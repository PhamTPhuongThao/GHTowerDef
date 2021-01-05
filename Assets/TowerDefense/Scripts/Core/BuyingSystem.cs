using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingSystem : MonoBehaviour
{
    public GameObject Mickey;
    public GameObject Ralph;

    public Transform launchPoint;
    public Patrol patrol;
    public TeamRight teamRight;
    public TeamLeft teamLeft;
    private Vector3 spawnPos;

    public NPCBloodBar NPCTeamBloodBar;
    public NPCBloodBar NPCEnemyBloodBar;
    public Canvas canvas;
    public HeroLoader HeroLoader;

    private float maxAngleDownRight = 3 * Mathf.PI / 2;
    private float maxAngleUp = Mathf.PI / 2;
    private float maxAngleDown = -Mathf.PI / 2;

    private bool isMickeyRecover;
    private bool isRalphRecover;
    public Slider recoverSliderOfMickey;
    public Slider recoverSliderOfRalph;
    private float waitingTime = 4f;
    private float counterMickey, counterRalph;

    public List<GameObject> ourTeamContainer;
    public List<GameObject> enemyTeamContainer;

    private void Start()
    {
        patrol = FindObjectOfType<Patrol>();
        teamRight = FindObjectOfType<TeamRight>();
        teamLeft = FindObjectOfType<TeamLeft>();

        isMickeyRecover = true;
        isRalphRecover = true;
        recoverSliderOfMickey.gameObject.SetActive(false);
        recoverSliderOfRalph.gameObject.SetActive(false);
        recoverSliderOfMickey.maxValue = waitingTime;
        recoverSliderOfRalph.maxValue = waitingTime;
    }

    private void Update()
    {
        recoverSliderOfMickey.value = counterMickey;
        recoverSliderOfRalph.value = counterRalph;
        if (!isMickeyRecover)
        {
            recoverSliderOfMickey.gameObject.SetActive(true);
            counterMickey -= Time.deltaTime;
            if (counterMickey <= 0)
            {
                counterMickey = waitingTime;
                recoverSliderOfMickey.gameObject.SetActive(false);
                isMickeyRecover = true;
            }
        }
        if (!isRalphRecover)
        {
            recoverSliderOfRalph.gameObject.SetActive(true);
            counterRalph -= Time.deltaTime;
            if (counterRalph <= 0)
            {
                counterRalph = waitingTime;
                recoverSliderOfRalph.gameObject.SetActive(false);
                isRalphRecover = true;
            }
        }

    }

    public void BuyMickey()
    {
        if (isMickeyRecover)
        {
            isMickeyRecover = false;
            counterMickey = waitingTime;
            BuyHero(Mickey, 50, false, true, null);
        }
    }

    public void BuyRalph()
    {
        if (isRalphRecover)
        {
            counterRalph = waitingTime;
            isRalphRecover = false;
            BuyHero(Ralph, 40, false, true, null);
        }
    }

    public void BuyHero(GameObject hero, int value, bool start, bool enemyOrOurTeam, HeroLoader.Hero heroClass)
    {
        if (CoinSystem.Instance.totalCoin < value)
        {
            return;
        }
        ChooseTeam(enemyOrOurTeam, hero);
        InstantiateHero(hero, value, start, enemyOrOurTeam);
        SetConfig(hero, start, heroClass);
        CreateLabel(enemyOrOurTeam, hero);

    }

    private void ChooseTeam(bool enemyOrOurTeam, GameObject hero)
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

    private void CreateLabel(bool enemyOrOurTeam, GameObject hero)
    {
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
        BloodBarCopy.transform.SetParent(canvas.GetComponentInChildren<HealthSystem>().transform, false);
        hero.GetComponent<NPC>().NPCBloodBar = BloodBarCopy.GetComponent<NPCBloodBar>();
    }

    private void InstantiateHero(GameObject hero, int value, bool start, bool enemyOrOurTeam)
    {
        var angle = Random.Range(maxAngleUp, maxAngleDown);
        var pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 4;
        spawnPos = launchPoint.position + pos;
        CoinSystem.Instance.SpendCoin(value);
        if (start)
        {
            var cur = Instantiate(hero, spawnPos, launchPoint.rotation);
            cur.GetComponent<Patrol>().enabled = false;
            if (enemyOrOurTeam)
            {
                ourTeamContainer.Add(cur);
            }
            else
            {
                enemyTeamContainer.Add(cur);
            }
        }
        else
        {
            Instantiate(hero, spawnPos, launchPoint.rotation);
        }
    }

    private void SetConfig(GameObject hero, bool start, HeroLoader.Hero heroClass)
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
