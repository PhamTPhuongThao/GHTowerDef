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
        public int hero;
        public int attackType;
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
    public HeroesCollectionOfOurTeam heroesCollectionOfOurTeam = new HeroesCollectionOfOurTeam();
    public HeroesCollectionOfEnemyTeam heroesCollectionOfEnemyTeam = new HeroesCollectionOfEnemyTeam();

    [ContextMenu("Load Heroes Configure")]
    private void Start()
    {
        heroesCollectionOfOurTeam = JsonUtility.FromJson<HeroesCollectionOfOurTeam>(textJSONOurTeam.text);
        heroesCollectionOfEnemyTeam = JsonUtility.FromJson<HeroesCollectionOfEnemyTeam>(textJSONEnemyTeam.text);
    }
    private void Update()
    {

    }
}
