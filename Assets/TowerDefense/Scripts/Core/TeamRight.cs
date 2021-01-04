using UnityEngine;
using UnityEngine.UI;

public class TeamRight : MonoBehaviour
{
    public int maxHP;
    private int value;
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
            // health bar of 2 team
            if (teamRightHealthBar && teamLeft.teamLeftHealthBar)
            {
                Destroy(teamRightHealthBar.gameObject);
                Destroy(teamLeft.teamLeftHealthBar.gameObject);
            }
            // health bar of NPC of 2 team
            if (NPCEnemyHealthBar && NPCEnemyHealthBar)
            {
                Destroy(NPCHealthBar.gameObject);
                Destroy(NPCEnemyHealthBar.gameObject);
            }
            // 2 team
            Destroy(this.gameObject);
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
