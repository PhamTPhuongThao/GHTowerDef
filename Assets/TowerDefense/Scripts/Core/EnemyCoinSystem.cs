using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCoinSystem : MonoBehaviour
{
    Text text;
    public static int totalCoin;

    // private static EnemyCoinSystem instance;

    // private EnemyCoinSystem() { }

    // // public static CoinSystem getInstance()
    // // {
    // //     if (instance == null)
    // //     {
    // //         instance = new CoinSystem();
    // //     }
    // //     return instance;
    // // }

    // public static EnemyCoinSystem Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             instance = new EnemyCoinSystem();
    //         }
    //         return instance;
    //     }
    // }

    void Start()
    {
        text = GetComponent<Text>();
        totalCoin = 500;
    }


    void Update()
    {
        if (totalCoin < 0)
        {
            totalCoin = 0;
        }
        text.text = totalCoin + "C";
    }

    public static void GetCoin(int coinToGet)
    {
        totalCoin += coinToGet;
    }

    public static void SpendCoin(int coinToSpend)
    {
        totalCoin -= coinToSpend;
    }
}
