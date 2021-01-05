using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private NPC nPC;
    private GameObject getHeroImage;
    private HeroLoader heroLoader;

    private void Start()
    {
        nPC = GetComponent<NPC>();
        heroLoader = FindObjectOfType<HeroLoader>();
    }

    public void OnMouseDown()
    {
        if ((nPC.isTeamright && !heroLoader.chooseTeamLeft) || (!nPC.isTeamright && heroLoader.chooseTeamLeft))
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            getHeroImage = Instantiate(nPC.heroImage, transform.position, transform.rotation);
            getHeroImage.GetComponent<ImageManager>().isTeamright = nPC.isTeamright;
            getHeroImage.GetComponent<ImageManager>().ownHero = transform.gameObject;
        }
        return;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (getHeroImage)
        {
            getHeroImage.transform.position = GetMouseWorldPos() + mOffset;
            if (getHeroImage.transform.position.z < 0)
            {
                return;
            }
        }
    }

    private void OnMouseUp()
    {
        if (getHeroImage && !getHeroImage.GetComponent<ImageManager>().canLevelUp)
        {
            Destroy(getHeroImage);
        }
    }

    public IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<Patrol>().animator.SetBool("canLevelUp", false);
    }

    public void ResetConfig()
    {
        var currChange = this.gameObject.GetComponent<NPC>();
        currChange.level++;
        currChange.MaxHp = currChange.MaxHp * 2;
        currChange.MaxAttack = currChange.MaxAttack * 2;
        currChange.AttackMiss = currChange.AttackMiss * 2;
        currChange.PhysicalDefense = currChange.PhysicalDefense * 2;
        currChange.CriticalChance = currChange.CriticalChance * 2;
        currChange.CriticalDamage = currChange.CriticalDamage * 2;
        currChange.AttackSpeed = currChange.AttackSpeed * 2;
        currChange.AttackType = currChange.AttackType * 2;
        StartCoroutine(Waiting());

    }

}
