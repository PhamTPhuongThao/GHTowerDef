using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    private NPC nPC;

    bool canLevelUp;
    private GameObject getHeroImage;

    private void Start()
    {
        nPC = GetComponent<NPC>();
    }

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        getHeroImage = Instantiate(nPC.heroImage, transform.position, transform.rotation);
        getHeroImage.GetComponent<ImageManager>().isTeamright = nPC.isTeamright;
        getHeroImage.GetComponent<ImageManager>().ownHero = transform.gameObject;
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
        }
    }

    private void OnMouseUp()
    {
        if (getHeroImage && !getHeroImage.GetComponent<ImageManager>().canLevelUp)
        {
            Destroy(getHeroImage);
        }
    }


    public void ResetConfig()
    {
        this.gameObject.GetComponent<NPC>().level++;
        // this.gameObject.GetComponent<NPC>().MaxHp = 
        // this.gameObject.GetComponent<NPC>().MaxAttack = 
        // this.gameObject.GetComponent<NPC>().AttackMiss = 
        // this.gameObject.GetComponent<NPC>().PhysicalDefense = 
        // this.gameObject.GetComponent<NPC>().CriticalChance = 
        // this.gameObject.GetComponent<NPC>().CriticalDamage = 
        // this.gameObject.GetComponent<NPC>().AttackSpeed = 
        // this.gameObject.GetComponent<NPC>().AttackType = 

    }




}
