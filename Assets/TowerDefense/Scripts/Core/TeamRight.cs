using UnityEngine;
using UnityEngine.UI;

public class TeamRight : MonoBehaviour
{
    public int maxHP;
    public int value;
    public HeroLoader heroLoader;
    public GameObject winScreen;
    public GameObject loseScreen;
    public TeamLeft teamLeft;

    private void Start()
    {
        heroLoader = FindObjectOfType<HeroLoader>();
        teamLeft = FindObjectOfType<TeamLeft>();
        value = 200;
    }

    public void GetHurt(int amountBlood)
    {
        maxHP -= amountBlood;
        if (maxHP < 0)
        {
            maxHP = 0;
            KillEnemyCoinGetting();
            Destroy(this.gameObject);
            Destroy(teamLeft.gameObject);
        }
    }

    public void KillEnemyCoinGetting()
    {
        if (heroLoader.chooseTeamLeft)
        {
            CoinSystem.GetCoin(value);
            winScreen.SetActive(true);
        }
        else
        {
            EnemyCoinSystem.GetCoin(value);
            loseScreen.SetActive(true);

        }
    }
}
