using UnityEngine;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour
{
    // public Text levelText;
    public Slider bloodslider;
    public RectTransform rectTransform;

    private void Start()
    {
        // levelText = transform.parent.GetComponent<NPC>().NPCLevelText.text;
        bloodslider = transform.parent.GetComponent<NPC>().NPCBloodBar.bloodBar;
    }
    void Update()
    {
        // if (levelText)
        // {
        //     Vector2 levelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //     levelText.rectTransform.anchoredPosition = (levelPos - new Vector2(420, 230)) * 4f;
        //     levelText.canvas.overrideSorting = true;
        // }
        if (bloodslider)
        {
            Vector2 PosSlider = Camera.main.WorldToScreenPoint(this.transform.position);
            bloodslider.GetComponent<RectTransform>().anchoredPosition = (PosSlider - new Vector2(930, 500));

        }
    }
}
