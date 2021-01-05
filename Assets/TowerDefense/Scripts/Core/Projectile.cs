using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public NPC ownPlayer;
    void Start()
    {
    }

    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ownPlayer.tag == "HeroLeft" && (other.tag == "HeroRight" || other.tag == "TowerRight" || other.tag == "Right"))
        {
            if (other.tag == "TowerRight")
            {
                other.GetComponent<TeamRight>().GetHurt(ownPlayer.GetComponent<NPC>().MaxAttack);
                Destroy(this.gameObject);
            }
            else
            {
                other.GetComponent<NPC>().GetHurt(ownPlayer.GetComponent<NPC>().MaxAttack);
                Destroy(gameObject);
            }
        }
        if (ownPlayer.tag == "HeroRight" && (other.tag == "HeroLeft" || other.tag == "TowerLeft" || other.tag == "Left"))
        {
            if (other.tag == "TowerLeft")
            {
                other.GetComponent<TeamLeft>().GetHurt(ownPlayer.GetComponent<NPC>().MaxAttack);
                Destroy(this.gameObject);
            }
            else
            {
                other.GetComponent<NPC>().GetHurt(ownPlayer.GetComponent<NPC>().MaxAttack);
                Destroy(this.gameObject);
            }
        }

    }
}
