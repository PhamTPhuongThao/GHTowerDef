using UnityEngine;
using UnityEngine.UI;


public class NPCBloodBar : MonoBehaviour
{
    public Slider bloodBar;

    public void Start()
    {
        bloodBar = GetComponent<Slider>();
    }

}

