using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public bool isTeamright;
    public bool canLevelUp;
    public GameObject ownHero;
    public bool occupied;

    private void Start()
    {
        occupied = false;
    }
    private void Update()
    {
        if (!ownHero)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        var ownerNPC = ownHero.GetComponent<NPC>();
        var otherNPC = other.GetComponent<NPC>();
        if (other.gameObject != ownHero && other.GetComponent<NPC>() && (ownerNPC.level == otherNPC.level) && (ownerNPC.Name == otherNPC.Name))
        {
            if (isTeamright && other.tag == "HeroRight")
            {
                this.gameObject.GetComponent<Collider>().enabled = false;
                LevelingUp(other);
            }
            else if (!isTeamright && other.tag == "HeroLeft")
            {
                this.gameObject.GetComponent<Collider>().enabled = false;
                LevelingUp(other);
            }

        }
    }

    public void LevelingUp(Collider other)
    {
        var ownHeroNPC = ownHero.GetComponent<NPC>();
        ownHeroNPC.isLevelingUp = true;
        Destroy(this.gameObject);
        Destroy(ownHero);
        if (ownHeroNPC.NPCBloodBar)
        {
            Destroy(ownHeroNPC.NPCBloodBar.gameObject);
        }
        other.GetComponent<NPC>().countAttack = 0;
        other.transform.localScale = other.GetComponent<NPC>().originalScale;
        other.GetComponent<Patrol>().animator.SetBool("canLevelUp", true);
        if (ownHeroNPC.AttackType == 1 || other.GetComponent<NPC>().AttackType == 1)
        {
            other.GetComponent<LevelUp>().ResetConfig(1);
        }
        else
        {
            other.GetComponent<LevelUp>().ResetConfig(0);
        }

    }
}
