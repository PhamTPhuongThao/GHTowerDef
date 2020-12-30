using UnityEngine;
using UnityEngine.UI;


public class NPCLevelText : MonoBehaviour
{
    public Text text;

    public void Start()
    {
        text = GetComponent<Text>();
    }

}

