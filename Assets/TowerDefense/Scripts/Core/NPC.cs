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


    public CenterLine center;
    public Patrol patrol;
    public GameObject heroImage;

    public LevelText levelText;
    public int level;
    public bool isLevelingUp;

    void Start()
    {
        center = FindObjectOfType<CenterLine>();
        patrol = GetComponent<Patrol>();
        level = 1;
        levelText = FindObjectOfType<LevelText>();
        isLevelingUp = false;
        if (levelText.text)
        {
            levelText.text.text = "L." + level;
        }

        if (transform.position.x > center.center.x)
        {
            isTeamright = true;
        }
        else
        {
            isTeamright = false;
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
