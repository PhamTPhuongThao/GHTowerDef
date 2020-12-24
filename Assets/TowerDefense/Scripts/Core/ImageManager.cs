using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public bool isTeamright;
    public GameObject ownHero;

    public bool canLevelUp;

    private void Update()
    {
        if (!ownHero)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != ownHero && other.GetComponent<NPC>())
        {
            if (isTeamright && other.tag == "HeroRight" && (ownHero.GetComponent<NPC>().level == other.GetComponent<NPC>().level) && (ownHero.GetComponent<NPC>().Name == other.GetComponent<NPC>().Name))
            {
                ownHero.GetComponent<NPC>().isLevelingUp = true;
                Destroy(this.gameObject);
                Destroy(ownHero);
                Destroy(ownHero.GetComponent<NPC>().levelText.gameObject);
                other.GetComponent<NPC>().level++;
                other.GetComponent<Patrol>().animator.SetBool("canLevelUp", true);
                other.GetComponent<LevelUp>().ResetConfig();
            }
            else if (!isTeamright && other.tag == "HeroLeft" && (ownHero.GetComponent<NPC>().level == other.GetComponent<NPC>().level) && (ownHero.GetComponent<NPC>().Name == other.GetComponent<NPC>().Name))
            {
                ownHero.GetComponent<NPC>().isLevelingUp = true;
                Destroy(this.gameObject);
                Destroy(ownHero);
                Destroy(ownHero.GetComponent<NPC>().levelText.gameObject);
                other.GetComponent<Patrol>().animator.SetBool("canLevelUp", true);
                other.GetComponent<LevelUp>().ResetConfig();
            }

        }

    }
}
