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
    public Rigidbody characterRigidbody;

    void Start()
    {
        center = FindObjectOfType<CenterLine>();
        characterRigidbody = GetComponent<Rigidbody>();
        if (transform.position.x > center.center.x)
        {
            isTeamright = true;
        }
        else
        {
            isTeamright = false;
        }
    }

    public void GetHurt(int amountBlood)
    {
        MaxHp -= amountBlood;
        if (MaxHp < 0)
        {
            MaxHp = 0;
            Destroy(this.gameObject);
        }
    }
}
