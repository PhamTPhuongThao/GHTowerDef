using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public NPC ownPlayer;
    public Collider enemy;
    public float bulletSpeed;
    void Start()
    {
        bulletSpeed = 16f;
    }

    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (enemy)
        {
            transform.position = Vector3.MoveTowards(transform.position + Vector3.up * 4 * Time.deltaTime + Vector3.forward * 2 * Time.deltaTime, enemy.transform.position, bulletSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ownPlayer && ownPlayer.tag == "HeroLeft" && (other.tag == "HeroRight" || other.tag == "TowerRight" || other.tag == "Right"))
        {
            if (other.tag == "TowerRight")
            {
                other.GetComponent<TeamRight>().GetHurt(ownPlayer.GetComponent<NPC>().MaxAttack);
                Destroy(this.gameObject);
            }
            else
            {
                if (other.GetComponent<NPC>())
                {
                    other.GetComponent<NPC>().GetHurt(ownPlayer.GetComponent<NPC>().MaxAttack);
                    Destroy(this.gameObject);
                }
            }
        }
        if (ownPlayer && ownPlayer.tag == "HeroRight" && (other.tag == "HeroLeft" || other.tag == "TowerLeft" || other.tag == "Left"))
        {
            if (other.tag == "TowerLeft")
            {
                other.GetComponent<TeamLeft>().GetHurt(ownPlayer.GetComponent<NPC>().MaxAttack);
                Destroy(this.gameObject);
            }
            else
            {
                if (other.GetComponent<NPC>())
                {
                    other.GetComponent<NPC>().GetHurt(ownPlayer.GetComponent<NPC>().MaxAttack);
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
