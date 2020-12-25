using System.Collections;
using System.Collections.Generic;
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
        if (!occupied)
        {
            occupied = true;
        }
        var ownerNPC = ownHero.GetComponent<NPC>();
        var otherNPC = other.GetComponent<NPC>();

        if (other.gameObject != ownHero && other.GetComponent<NPC>() && (ownerNPC.level == otherNPC.level) && (ownerNPC.Name == otherNPC.Name))
        {
            if (isTeamright && other.tag == "HeroRight")
            {
                LevelingUp(other);
            }
            else if (!isTeamright && other.tag == "HeroLeft")
            {
                LevelingUp(other);
            }

        }
        occupied = false;
    }

    public void LevelingUp(Collider other)
    {
        var otherNPC = ownHero.GetComponent<NPC>();
        otherNPC.isLevelingUp = true;
        Destroy(this.gameObject);
        Destroy(ownHero);
        if (otherNPC.levelText)
        {
            Destroy(otherNPC.levelText.gameObject);
        }
        other.GetComponent<Patrol>().animator.SetBool("canLevelUp", true);
        other.GetComponent<LevelUp>().ResetConfig();
    }
}
