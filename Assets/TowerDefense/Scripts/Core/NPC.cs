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
    public GameObject skillEffect;
    public GameObject getHitParticle;
    public GameObject bullet;

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
            NPCBloodBar.bloodBar.maxValue = MaxHp;
            NPCBloodBar.bloodBar.value = MaxHp;
        }
        else if (this.gameObject.tag == "Left" || this.gameObject.tag == "Right")
        {
            MaxHp = 100;
            MovementSpeed = 4;
            MaxAttack = 10;
            value = 20;
            NPCBloodBar = null;
            NPCLevelText = null;
            heroImage = null;
            skillEffect = null;
        }
        originalScale = this.transform.localScale;
    }

    private void Update()
    {
        if (this.gameObject.tag == "HeroLeft" || this.gameObject.tag == "HeroRight")
        {
            LevelUpdate();
            BloodUpdate();
        }
        AttackSpeedCal();
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
        var attackContainer = MaxAttack;
        if (canAttack)
        {
            if (AttackType == 0)
            {
                DoingAttack(enemy, attackContainer, 0);
            }
            else if (AttackType == 1 && (this.tag == "HeroRight" || this.tag == "HeroLeft"))
            {

                DoingFarAttack(enemy, attackContainer, 0);
            }
            if (enemy)
            {
                patrol.transform.LookAt(enemy.transform);
            }
        }
    }

    public void AttackTower(Collider enemy)
    {
        var attackContainer = MaxAttack;

        if (isTeamright && canAttack)
        {
            if (AttackType == 0)
            {
                DoingAttack(enemy, attackContainer, 1);
            }
            else if (AttackType == 1 && (this.tag == "HeroRight"))
            {
                DoingFarAttack(enemy, attackContainer, 1);
            }
        }
        else if (!isTeamright && canAttack)
        {
            if (AttackType == 0)
            {
                DoingAttack(enemy, attackContainer, -1);
            }
            else if (AttackType == 1 && (this.tag == "HeroLeft"))
            {
                DoingFarAttack(enemy, attackContainer, -1);
            }
        }
        if (enemy)
        {
            patrol.transform.LookAt(enemy.transform);
        }
    }

    public IEnumerator WaitingDoingSkill()
    {
        yield return new WaitForSeconds(5f);
        patrol.animator.SetBool("doingSkill", false);
    }

    public void DoingAttack(Collider enemy, int attackContainer, int classToChoose)
    {
        if (countAttack == (int)(1 / CriticalChance) && countAttack != 0 && (this.Name == "Mickey" || this.Name == "Ralph"))
        {

            if ((heroLoader.chooseTeamLeft && this.tag == "HeroLeft") || (!heroLoader.chooseTeamLeft && this.tag == "HeroRight"))
            {
                patrol.animator.SetBool("doingSkill", true);
            }
            MaxAttack = (int)(MaxAttack * CriticalDamage);
            countAttack = 0;
        }
        if (classToChoose == 0)
        {
            if (enemy)
            {
                enemy.GetComponent<NPC>().GetHurt(MaxAttack);
            }
        }
        if (classToChoose == 1)
        {
            enemy.GetComponent<TeamLeft>().GetHurt(MaxAttack);
        }
        if (classToChoose == -1)
        {
            enemy.GetComponent<TeamRight>().GetHurt(MaxAttack);
        }
        canAttack = false;
        StartCoroutine(WaitingDoingSkill());
        MaxAttack = attackContainer;
        countAttack++;
    }

    public void DoingFarAttack(Collider enemy, int attackContainer, int classToChoose)
    {
        if (countAttack == (int)(1 / CriticalChance) && countAttack != 0 && (this.Name == "Mickey" || this.Name == "Ralph"))
        {
            if ((heroLoader.chooseTeamLeft && this.tag == "HeroLeft") || (!heroLoader.chooseTeamLeft && this.tag == "HeroRight"))
            {
                patrol.animator.SetBool("doingSkill", true);
            }
            MaxAttack = (int)(MaxAttack * CriticalDamage);
            countAttack = 0;
        }
        var positionToStart = transform.GetComponentInChildren<BulletStart>().GetComponent<SphereCollider>().transform.position;
        Debug.Log(transform.GetComponentInChildren<BulletStart>());
        if (classToChoose == 0)
        {
            if (enemy)
            {
                var bulletImage = Instantiate(bullet, positionToStart, transform.rotation);
                bulletImage.GetComponent<Projectile>().ownPlayer = this;
                bulletImage.GetComponent<Projectile>().enemy = enemy;
            }
        }
        if (classToChoose == 1)
        {
            var bulletImage = Instantiate(bullet, positionToStart, transform.rotation);
            bulletImage.GetComponent<Projectile>().ownPlayer = this;
            bulletImage.GetComponent<Projectile>().enemy = enemy;
        }
        if (classToChoose == -1)
        {
            var bulletImage = Instantiate(bullet, positionToStart, transform.rotation);
            bulletImage.GetComponent<Projectile>().ownPlayer = this;
            bulletImage.GetComponent<Projectile>().enemy = enemy;
        }
        canAttack = false;
        StartCoroutine(WaitingDoingSkill());
        MaxAttack = attackContainer;
        countAttack++;
    }


    public void GetHurt(int amountBlood)
    {
        Instantiate(getHitParticle, transform.position + Vector3.up * 2, transform.rotation);
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
