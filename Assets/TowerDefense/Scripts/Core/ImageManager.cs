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
