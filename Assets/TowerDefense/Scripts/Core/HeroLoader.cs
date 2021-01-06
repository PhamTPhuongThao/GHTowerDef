using UnityEngine;

public class HeroLoader : MonoBehaviour
{
    [SerializeField]
    public TextAsset textJSONOurTeam;
    public TextAsset textJSONEnemyTeam;
    public TextAsset textJSONArmy;

    [System.Serializable]
    public class Hero
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
    }

    public class Soldier
    {
        public int NumberOfArmy;
        public int MaxHp;
        public int MaxAttack;
        public int AttackMiss;
        public int PhysicalDefense;
        public float CriticalChance;
        public float CriticalDamage;
        public float AttackSpeed;
        public int AttackType;
        public int MovementSpeed;
    }

    [System.Serializable]
    public class HeroesCollectionOfOurTeam
    {
        public int numberOfHero;
        public Hero[] heroes;

    }
    public class HeroesCollectionOfEnemyTeam
    {
        public int numberOfHero;
        public Hero[] heroes;

    }

    public bool chooseTeamLeft;
    public HeroesCollectionOfOurTeam heroesCollectionOfOurTeam = new HeroesCollectionOfOurTeam();
    public HeroesCollectionOfEnemyTeam heroesCollectionOfEnemyTeam = new HeroesCollectionOfEnemyTeam();
    public Soldier soldier = new Soldier();

    [ContextMenu("Load Heroes Configure")]
    private void Start()
    {
        chooseTeamLeft = false;
        heroesCollectionOfOurTeam = JsonUtility.FromJson<HeroesCollectionOfOurTeam>(textJSONOurTeam.text);
        heroesCollectionOfEnemyTeam = JsonUtility.FromJson<HeroesCollectionOfEnemyTeam>(textJSONEnemyTeam.text);
        soldier = JsonUtility.FromJson<Soldier>(textJSONArmy.text);
    }
}
