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
        if (other.gameObject != ownHero)
        {

            if (isTeamright && other.tag == "HeroRight" && (ownHero.GetComponent<NPC>().level == other.GetComponent<NPC>().level))// + 2 hero different
            {
                canLevelUp = true;
                Destroy(this.gameObject);
                Destroy(ownHero);
                Debug.Log("can level up");
                other.GetComponent<Patrol>().animator.SetBool("canLevelUp", canLevelUp);
                other.GetComponent<LevelUp>().ResetConfig();
            }
            else if (!isTeamright && other.tag == "HeroLeft" && (ownHero.GetComponent<NPC>().level == other.GetComponent<NPC>().level))
            {
                canLevelUp = true;
                Destroy(this.gameObject);
                Destroy(ownHero);
                Debug.Log("can level up");
                other.GetComponent<Patrol>().animator.SetBool("canLevelUp", canLevelUp);
                other.GetComponent<LevelUp>().ResetConfig();
            }
            canLevelUp = false;
        }

    }
}
