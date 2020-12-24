using UnityEngine;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour
{
    public Text levelLabel;
    private void Start()
    {
        levelLabel = transform.parent.GetComponent<NPC>().levelText.text;
    }
    void Update()
    {
        if (levelLabel)
        {
            Vector3 levelPos = Camera.main.WorldToScreenPoint(this.transform.position);
            levelLabel.transform.position = levelPos;
        }
    }
}
