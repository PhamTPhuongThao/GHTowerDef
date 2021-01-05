using UnityEngine;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour
{
    public Slider bloodslider;
    public RectTransform rectTransform;

    private void Start()
    {
        bloodslider = transform.parent.GetComponent<NPC>().NPCBloodBar.bloodBar;
    }
    void Update()
    {
        if (bloodslider)
        {
            Vector2 PosSlider = Camera.main.WorldToScreenPoint(this.transform.position);
            bloodslider.GetComponent<RectTransform>().anchoredPosition = (PosSlider - new Vector2(930, 500));

        }
    }
}
