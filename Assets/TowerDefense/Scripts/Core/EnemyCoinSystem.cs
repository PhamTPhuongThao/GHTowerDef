using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCoinSystem : MonoBehaviour
{
    Text text;
    public int totalCoin;

    private static EnemyCoinSystem m_Instance;

    private static readonly object m_Lock = new object();

    public static EnemyCoinSystem Instance
    {
        get
        {
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (EnemyCoinSystem)FindObjectOfType(typeof(EnemyCoinSystem));

                    if (FindObjectsOfType(typeof(EnemyCoinSystem)).Length > 1)
                    {
                        return m_Instance;
                    }

                    if (m_Instance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_Instance = singleton.AddComponent<EnemyCoinSystem>();
                        singleton.name = string.Format("[------Singleton: {0}-----] ", typeof(EnemyCoinSystem).Name);
                        DontDestroyOnLoad(singleton);
                    }
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
