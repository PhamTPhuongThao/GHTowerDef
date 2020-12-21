using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class HeroLoader : MonoBehaviour
{
    [SerializeField]
    public TextAsset textJSONOurTeam;
    public TextAsset textJSONEnemyTeam;

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

    [System.Serializable]
    public class HeroesCollectionOfOurTeam
    {
        public int numberOfHero;
        public Hero[] heroes;
        public int numberOfArmy;

    }
    public class HeroesCollectionOfEnemyTeam
    {
        public int numberOfHero;
        public Hero[] heroes;
        public int numberOfArmy;

    }

    public WarManager warManager;
    public HeroesCollectionOfOurTeam heroesCollectionOfOurTeam = new HeroesCollectionOfOurTeam();
    public HeroesCollectionOfEnemyTeam heroesCollectionOfEnemyTeam = new HeroesCollectionOfEnemyTeam();

    [ContextMenu("Load Heroes Configure")]
    private void Start()
    {
        warManager = FindObjectOfType<WarManager>();
        heroesCollectionOfOurTeam = JsonUtility.FromJson<HeroesCollectionOfOurTeam>(textJSONOurTeam.text);
        heroesCollectionOfEnemyTeam = JsonUtility.FromJson<HeroesCollectionOfEnemyTeam>(textJSONEnemyTeam.text);
        warManager.StartGame();
    }
    private void Update()
    {

    }
}
