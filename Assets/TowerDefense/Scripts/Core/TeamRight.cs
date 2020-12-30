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
    public NPCBloodBar teamRightHealthBar;
    public NPCBloodBar NPCHealthBar;
    public NPCBloodBar NPCEnemyHealthBar;

    private void Start()
    {
        heroLoader = FindObjectOfType<HeroLoader>();
        teamLeft = FindObjectOfType<TeamLeft>();
        value = 200;
        maxHP = 1500;
        teamRightHealthBar.bloodBar.maxValue = maxHP;
    }

    private void Update()
    {
        teamRightHealthBar.bloodBar.value = maxHP;
    }

    public void GetHurt(int amountBlood)
    {
        maxHP -= amountBlood;
        if (maxHP < 0)
        {
            maxHP = 0;
            KillEnemyCoinGetting();
            Destroy(this.gameObject);
            Destroy(teamRightHealthBar.gameObject);
            Destroy(teamLeft.teamLeftHealthBar.gameObject);
            Destroy(NPCHealthBar.gameObject);
            Destroy(NPCEnemyHealthBar.gameObject);
            Destroy(teamLeft.gameObject);
        }
    }

    public void KillEnemyCoinGetting()
    {
        if (heroLoader.chooseTeamLeft)
        {
            CoinSystem.Instance.GetCoin(value);
            winScreen.SetActive(true);
        }
        else
        {
            EnemyCoinSystem.Instance.GetCoin(value);
            loseScreen.SetActive(true);

        }
    }
}
