using UnityEngine;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour
{
    public Text levelText;
    public Canvas canvas;

    private void Start()
    {
        levelText = transform.parent.GetComponent<NPC>().levelText.text;
    }
    void Update()
    {
        if (levelText)
        {
            Vector2 levelPos = Camera.main.WorldToScreenPoint(this.transform.position);
            Debug.Log(levelPos);
            levelText.rectTransform.anchoredPosition = levelPos;
        }
    }
}
