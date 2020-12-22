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
    public int blood;
    public bool isTeamright;
    public CenterLine center;
    public Rigidbody characterRigidbody;

    void Start()
    {
        // MaxHp = 100;
        // MaxAttack = 10;
        // AttackMiss = 0;
        // PhysicalDefense = 30;
        // CriticalChance = 0f;
        // CriticalDamage = 0f;
        // AttackSpeed = 1f;
        // AttackType = 0;
        // MovementSpeed = 6;

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

    void Update()
    {

    }
}
