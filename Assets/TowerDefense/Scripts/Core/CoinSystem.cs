using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour
{
    Text text;
    public static int totalCoin;

    // private static CoinSystem instance;

    // private CoinSystem() { }

    // // public static CoinSystem getInstance()
    // // {
    // //     if (instance == null)
    // //     {
    // //         instance = new CoinSystem();
    // //     }
    // //     return instance;
    // // }

    // public static CoinSystem Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             instance = new CoinSystem();
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
