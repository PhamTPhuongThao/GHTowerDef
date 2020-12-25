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

    void Start()
    {
        patrol = GetComponent<Patrol>();
        level = 1;
        levelText = FindObjectOfType<LevelText>();
        isLevelingUp = false;

        if (levelText.text)
        {
            levelText.text.text = "L." + level;
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
            Destroy(this.gameObject);
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
