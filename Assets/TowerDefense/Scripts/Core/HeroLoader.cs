using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class HeroLoader : MonoBehaviour
{
    [SerializeField]
    public TextAsset textJSON;

    [System.Serializable]
    public class Hero
    {
        public int hero;
        public int attackType;
    }

    [System.Serializable]
    public class HeroesCollection
    {
        public int numberOfHero;
        public Hero[] heroes;

    }
    public HeroesCollection heroesCollection = new HeroesCollection();

    [ContextMenu("Load Heroes Configure")]
    private void Start()
    {
        heroesCollection = JsonUtility.FromJson<HeroesCollection>(textJSON.text);
    }
    private void Update()
    {

    }
}
