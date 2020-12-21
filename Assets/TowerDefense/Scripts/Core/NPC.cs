using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int value;
    public int blood;
    public bool teamright;
    public CenterLine center;

    void Start()
    {
        center = FindObjectOfType<CenterLine>();
        if (transform.position.x > center.center.x)
        {
            teamright = true;
        }
        else
        {
            teamright = false;
        }
    }

    void Update()
    {

    }
}
