using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelText : MonoBehaviour
{
    public Text text;

    public void Start()
    {
        text = GetComponent<Text>();
        text.text = "L." + 1;
    }

}
