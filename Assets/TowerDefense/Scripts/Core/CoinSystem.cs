using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour
{
    Text text;
    public int totalCoin;

    private static CoinSystem m_Instance;

    private static readonly object m_Lock = new object();

    public static CoinSystem Instance
    {
        get
        {
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (CoinSystem)FindObjectOfType(typeof(CoinSystem));

                    if (FindObjectsOfType(typeof(CoinSystem)).Length > 1)
                    {
                        // Debug.LogError("[Singleton] Something went really wrong " +
                        //                " - there should never be more than 1 singleton!" +
                        //                " Reopening the scene might fix it.");
                        return m_Instance;
                    }

                    if (m_Instance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_Instance = singleton.AddComponent<CoinSystem>();
                        singleton.name = string.Format("[------Singleton: {0}-----] ", typeof(CoinSystem).Name);

                        DontDestroyOnLoad(singleton);

                        // Debug.Log("[Singleton] An instance of " + typeof(CoinSystem) +
                        //           " is needed in the scene, so '" + singleton +
                        //           "' was created with DontDestroyOnLoad.");
                    }
                    // else
                    // {
                    //     Debug.Log("[Singleton] Using instance already created: " +
                    //               m_Instance.gameObject.name);
                    // }
                }

                return m_Instance;
            }
        }
    }

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

    public void GetCoin(int coinToGet)
    {
        totalCoin += coinToGet;
    }

    public void SpendCoin(int coinToSpend)
    {
        totalCoin -= coinToSpend;
    }

}
