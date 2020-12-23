using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    private NPC nPC;

    bool mouseButtonReleased;
    bool canLevelUp;

    private void Start()
    {
        nPC = GetComponent<NPC>();
    }
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        mouseButtonReleased = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        // create a copy of transform -> if level up delete main transform
        //Instantiate(transform.gameObject, transform.position, transform.rotation);
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void OnMouseUp()
    {
        mouseButtonReleased = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mouseButtonReleased)
        {
            if (nPC.isTeamright && other.tag == "HeroRight")
            {
                canLevelUp = true;
                Debug.Log("can level up");
            }
            else if (!nPC.isTeamright && other.tag == "HeroLeft")
            {
                canLevelUp = true;
                Debug.Log("can level up");
            }
        }
    }

    private void ResetConfi(Collider other)
    {

    }




}
