using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string Name;
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
    public bool isTeamright;

    public Patrol patrol;
    public GameObject heroImage;
    public GameObject effect;

    public NPCLevelText NPCLevelText;
    public NPCBloodBar NPCBloodBar;
    public int level;
    public bool isLevelingUp;
    public HeroLoader heroLoader;
    public bool canAttack;
    public float waiterForAttack;

    public int countAttack;

    public Vector3 originalScale;


    void Start()
    {
        patrol = GetComponent<Patrol>();
        NPCLevelText = FindObjectOfType<NPCLevelText>();
        NPCBloodBar = FindObjectOfType<NPCBloodBar>();
        heroLoader = FindObjectOfType<HeroLoader>();
        level = 1;
        isLevelingUp = false;
        if (this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "HeroRight")
        {
            value = 50;
            NPCBloodBar.bloodBar.maxValue = 750;
            NPCBloodBar.bloodBar.value = 750;
        }
        else if (this.gameObject.tag == "Left" || this.gameObject.tag == "Right")
        {
            value = 20;
            NPCBloodBar = null;
            NPCLevelText = null;
            heroImage = null;
            effect = null;
        }
        originalScale = this.transform.localScale;
    }

    private void Update()
    {
        if (this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "HeroRight")
        {
            LevelUpdate();
            BloodUpdate();
            AttackSpeedCal();
        }
    }

    public void AttackSpeedCal()
    {
        waiterForAttack += Time.deltaTime;
        if (waiterForAttack >= AttackSpeed)
        {
            canAttack = true;
            waiterForAttack = 0f;
        }
    }

    public void Attack(Collider enemy)
    {
        var skillEffect = effect;
        var attackContainer = MaxAttack;
        if (canAttack)
        {
            if (countAttack == (int)(1 / CriticalChance) && countAttack != 0 && (this.Name == "Mickey" || this.Name == "Ralph"))
            {
                MaxAttack = (int)(MaxAttack * CriticalDamage);
                skillEffect = Instantiate(effect, this.transform.position, this.transform.rotation);
                countAttack = 0;
                this.transform.localScale = originalScale;
            }
            enemy.GetComponent<NPC>().GetHurt(MaxAttack);
            canAttack = false;
            MaxAttack = attackContainer;
            countAttack++;
            // Destroy(skillEffect.gameObject);
            this.transform.localScale = originalScale + Vector3.one * (countAttack / ((float)2 * (1 / CriticalChance)));
        }
    }

    public void AttackTower(Collider enemy)
    {
        var skillEffect = effect;
        var attackContainer = MaxAttack;
        if (isTeamright && canAttack)
        {
            if (countAttack == (int)(1 / CriticalChance) && countAttack != 0 && (this.Name == "Mickey" || this.Name == "Ralph"))
            {
                MaxAttack = (int)(MaxAttack * CriticalDamage);
                skillEffect = Instantiate(effect, this.transform.position, this.transform.rotation);
                countAttack = 0;
                this.transform.localScale = originalScale;
            }
            enemy.GetComponent<TeamLeft>().GetHurt(MaxAttack);
            canAttack = false;
            MaxAttack = attackContainer;
            countAttack++;
            // Destroy(skillEffect.gameObject);
            this.transform.localScale = originalScale + Vector3.one * (countAttack / ((float)2 * (1 / CriticalChance)));
        }
        else if (!isTeamright && canAttack)
        {
            if (countAttack == (int)(1 / CriticalChance) && countAttack != 0 && (this.Name == "Mickey" || this.Name == "Ralph"))
            {
                MaxAttack = (int)(MaxAttack * CriticalDamage);
                skillEffect = Instantiate(effect, this.transform.position, this.transform.rotation);
                countAttack = 0;
                this.transform.localScale = originalScale;
            }
            enemy.GetComponent<TeamRight>().GetHurt(MaxAttack);
            canAttack = false;
            MaxAttack = attackContainer;
            countAttack++;
            // Destroy(skillEffect.gameObject);
            this.transform.localScale = originalScale + Vector3.one * (countAttack / ((float)2 * (1 / CriticalChance)));
        }
    }

    public void GetHurt(int amountBlood)
    {
        MaxHp -= amountBlood;
        if (MaxHp < 0)
        {
            MaxHp = 0;
            patrol.isDead = true;
            KillEnemyCoinGetting();
            if (NPCBloodBar)
            {
                Destroy(NPCBloodBar.gameObject);
            }
            Destroy(this.gameObject);

        }
    }

    public void KillEnemyCoinGetting()
    {
        if (heroLoader.chooseTeamLeft)
        {
            if ((this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "Left"))
            {
                EnemyCoinSystem.Instance.GetCoin(value);
            }
            else if ((this.gameObject.tag == "HeroRight" || this.gameObject.tag == "Right"))
            {
                CoinSystem.Instance.GetCoin(value);
            }
        }
        else
        {
            if ((this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "Left"))
            {
                CoinSystem.Instance.GetCoin(value);
            }
            else if ((this.gameObject.tag == "HeroRight" || this.gameObject.tag == "Right"))
            {
                EnemyCoinSystem.Instance.GetCoin(value);
            }
        }
    }

    public void LevelUpdate()
    {
        if (NPCLevelText.text)
        {
            NPCLevelText.text.text = "L." + level;
        }
    }

    public void BloodUpdate()
    {
        if (NPCBloodBar.bloodBar)
        {
            NPCBloodBar.bloodBar.value = MaxHp;
        }
    }

}
