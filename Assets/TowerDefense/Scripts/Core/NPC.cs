using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
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

    void Start()
    {
        center = FindObjectOfType<CenterLine>();
        patrol = GetComponent<Patrol>();
        if (transform.position.x > center.center.x)
        {
            isTeamright = true;
        }
        else
        {
            isTeamright = false;
        }
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

    }

}
