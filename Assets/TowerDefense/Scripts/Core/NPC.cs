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

    public LevelText levelText;
    public int level;
    public bool isLevelingUp;
    public HeroLoader heroLoader;

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
        LevelUp();
    }

    public void Attack(Collider enemy)
    {
        enemy.GetComponent<NPC>().GetHurt(MaxAttack);
    }

    public void AttackTower(Collider enemy)
    {
        if (isTeamright)
        {
            enemy.GetComponent<TeamLeft>().GetHurt(MaxAttack);
        }
        else
        {
            enemy.GetComponent<TeamRight>().GetHurt(MaxAttack);
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
        if (heroLoader.chooseTeamLeft) // we choose team left
        {
            if ((this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "Left"))
            {
                EnemyCoinSystem.GetCoin(value);
            }
            else if ((this.gameObject.tag == "HeroRight" || this.gameObject.tag == "Right"))
            {
                CoinSystem.GetCoin(value);
            }
        }
        else // we choose team right
        {
            if ((this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "Left"))
            {
                CoinSystem.GetCoin(value);
            }
            else if ((this.gameObject.tag == "HeroRight" || this.gameObject.tag == "Right"))
            {
                Debug.Log("hello");
                EnemyCoinSystem.GetCoin(value);
            }
        }
    }

    public int GetHP()
    {
        return MaxHp;
    }

    public void LevelUp()
    {
        if (levelText.text)
        {
            levelText.text.text = "L." + level;
        }
    }

}
