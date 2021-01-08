using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingSystem : MonoBehaviour
{
    public GameObject Mickey;
    public GameObject Ralph;

    public Transform launchPoint;
    // public Patrol patrol;
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

    public HeroLoader heroLoader;
    private HeroLoader.Hero MickeyConfig;
    private HeroLoader.Hero RalphConfig;

    private void Start()
    {
        // patrol = FindObjectOfType<Patrol>();
        teamRight = FindObjectOfType<TeamRight>();
        teamLeft = FindObjectOfType<TeamLeft>();

        isMickeyRecover = true;
        isRalphRecover = true;
        //StartCoroutine(Waiting());
        recoverSliderOfMickey.gameObject.SetActive(false);
        recoverSliderOfRalph.gameObject.SetActive(false);
        recoverSliderOfMickey.maxValue = waitingTime;
        recoverSliderOfRalph.maxValue = waitingTime;

        if (heroLoader.heroesCollectionOfOurTeam.heroes.Length > 0)
        {
            for (int i = 0; i < heroLoader.heroesCollectionOfOurTeam.heroes.Length; i++)
            {
                if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Mickey")
                {
                    MickeyConfig = heroLoader.heroesCollectionOfOurTeam.heroes[i];
                }
                else if (heroLoader.heroesCollectionOfOurTeam.heroes[i].Name == "Ralph")
                {
                    RalphConfig = heroLoader.heroesCollectionOfOurTeam.heroes[i];
                }
            }
        }
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

    // public IEnumerator Waiting()
    // {
    //     yield return new WaitForSeconds(3f);
    //     isMickeyRecover = false;
    //     isRalphRecover = false;
    // }

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
        SetConfig(hero, start, enemyOrOurTeam, heroClass);
        InstantiateHero(hero, value, start, enemyOrOurTeam);
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
        else
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
            StartCoroutine(Waiting(cur));
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
            var cur = Instantiate(hero, spawnPos, launchPoint.rotation);
            StartCoroutine(Waiting(cur));
        }

    }

    private IEnumerator Waiting(GameObject cur)
    {
        yield return new WaitForSeconds(.4f);
        cur.GetComponent<Patrol>().navMeshAgent.isStopped = true;
        cur.GetComponent<Patrol>().animator.SetBool("appear", true);
        yield return new WaitForSeconds(2f);
        if (cur != null && cur.GetComponent<Patrol>() != null && cur.GetComponent<Patrol>().animator != null && cur.GetComponent<Patrol>().navMeshAgent != null)
        {
            cur.GetComponent<Patrol>().animator.SetBool("appear", false);
            yield return new WaitForSeconds(.5f);
            if (cur != null && cur.GetComponent<Patrol>() != null && cur.GetComponent<Patrol>().animator != null && cur.GetComponent<Patrol>().navMeshAgent != null)
            {
                cur.GetComponent<Patrol>().navMeshAgent.isStopped = false;
            }
        }
    }

    private void SetConfig(GameObject hero, bool start, bool enemyOrOurTeam, HeroLoader.Hero heroClass)
    {
        var currNPC = hero.GetComponent<NPC>();
        if (hero == Mickey)
        {
            currNPC.Name = "Mickey";
            if (heroClass != null)
            {
                MickeyConfig = heroClass;
            }
        }
        else if (hero == Ralph)
        {
            currNPC.Name = "Ralph";
            if (heroClass != null)
            {
                RalphConfig = heroClass;
            }
        }
        if (enemyOrOurTeam)
        {
            if (currNPC.Name == "Mickey")
            {
                AddConfigSBS(currNPC, MickeyConfig);
            }
            else
            {
                AddConfigSBS(currNPC, RalphConfig);
            }
        }
        else
        {
            currNPC.MovementSpeed = 3;
            currNPC.MaxHp = 500;
            currNPC.MaxAttack = 20;
            currNPC.AttackMiss = 0;
            currNPC.PhysicalDefense = 20;
            currNPC.CriticalChance = 0.1f;
            currNPC.CriticalDamage = 1.2f;
            currNPC.AttackSpeed = 0.5f;
            currNPC.AttackType = 0;
            currNPC = null;
        }
    }

    private void AddConfigSBS(NPC currNPC, HeroLoader.Hero heroClass)
    {
        currNPC.MovementSpeed = heroClass.MovementSpeed;
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
