using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string Name;
    public int MaxHp;
    public int MaxAttack;
    public int AttackMiss;
    public int PhysicalDefense;
    public float CriticalChance;
    public float CriticalDamage;
    public float AttackSpeed;
    public int AttackType;
    public int MovementSpeed;

    public int value;
    public bool isTeamright;

    public Patrol patrol;
    public GameObject heroImage;
    public GameObject effect;

    public LevelText levelText;
    public int level;
    public bool isLevelingUp;
    public HeroLoader heroLoader;
    public bool canAttack;
    public float waiterForAttack;

    public int countAttack;


    void Start()
    {
        patrol = GetComponent<Patrol>();
        levelText = FindObjectOfType<LevelText>();
        heroLoader = FindObjectOfType<HeroLoader>();
        level = 1;
        isLevelingUp = false;
        if (this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "HeroRight")
        {
            value = 50;
        }
        else if (this.gameObject.tag == "Left" || this.gameObject.tag == "Right")
        {
            value = 20;
        }
    }

    private void Update()
    {
        LevelUpdate();
        AttackSpeedCal();
    }

    public void AttackSpeedCal()
    {
        waiterForAttack += Time.deltaTime;
        if (waiterForAttack >= AttackSpeed)
        {
            canAttack = true;
            waiterForAttack = 0f;
        }
    }

    public void Attack(Collider enemy)
    {
        var attackContainer = MaxAttack;
        if (canAttack)
        {
            if (countAttack == (int)(1 / CriticalChance) && countAttack != 0 && (this.Name == "Mickey" || this.Name == "Ralph"))
            {
                Debug.Log(countAttack);
                MaxAttack = (int)(MaxAttack * CriticalDamage);
                Instantiate(effect, this.transform.position, this.transform.rotation);
                countAttack = 0;
            }
            enemy.GetComponent<NPC>().GetHurt(MaxAttack);
            canAttack = false;
            MaxAttack = attackContainer;
            countAttack++;
        }
    }

    public void AttackTower(Collider enemy)
    {
        var attackContainer = MaxAttack;
        if (isTeamright && canAttack)
        {
            if (countAttack == (int)(1 / CriticalChance) && countAttack != 0 && (this.Name == "Mickey" || this.Name == "Ralph"))
            {
                MaxAttack = (int)(MaxAttack * CriticalDamage);
                Instantiate(effect, this.transform.position, this.transform.rotation);
                countAttack = 0;
            }
            enemy.GetComponent<TeamLeft>().GetHurt(MaxAttack);
            canAttack = false;
            countAttack++;
        }
        else if (!isTeamright && canAttack)
        {
            if (countAttack == (int)(1 / CriticalChance) && countAttack != 0 && (this.Name == "Mickey" || this.Name == "Ralph"))
            {
                MaxAttack = (int)(MaxAttack * CriticalDamage);
                Instantiate(effect, this.transform.position, this.transform.rotation);
                countAttack = 0;
            }
            enemy.GetComponent<TeamRight>().GetHurt(MaxAttack);
            canAttack = false;
            countAttack++;
        }
    }

    public void GetHurt(int amountBlood)
    {
        MaxHp -= amountBlood;
        if (MaxHp < 0)
        {
            MaxHp = 0;
            patrol.isDead = true;
            KillEnemyCoinGetting();
            Destroy(this.gameObject);

        }
    }

    public void KillEnemyCoinGetting()
    {
        if (heroLoader.chooseTeamLeft)
        {
            if ((this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "Left"))
            {
                EnemyCoinSystem.Instance.GetCoin(value);
            }
            else if ((this.gameObject.tag == "HeroRight" || this.gameObject.tag == "Right"))
            {
                CoinSystem.Instance.GetCoin(value);
            }
        }
        else
        {
            if ((this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "Left"))
            {
                CoinSystem.Instance.GetCoin(value);
            }
            else if ((this.gameObject.tag == "HeroRight" || this.gameObject.tag == "Right"))
            {
                EnemyCoinSystem.Instance.GetCoin(value);
            }
        }
    }

    public void LevelUpdate()
    {
        if (levelText.text)
        {
            levelText.text.text = "L." + level;
        }
    }

}
