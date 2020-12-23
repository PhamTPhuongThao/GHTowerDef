using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamRight : MonoBehaviour
{
    public int maxHP;

    public void GetHurt(int amountBlood)
    {
        maxHP -= amountBlood;
        if (maxHP < 0)
        {
            maxHP = 0;
            Destroy(this.gameObject);
        }
    }
}
