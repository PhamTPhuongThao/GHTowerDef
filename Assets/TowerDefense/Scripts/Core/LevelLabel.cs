using UnityEngine;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour
{
    public Text levelText;
    // public RectTransform UI_Element;
    // public RectTransform CanvasRect;
    // public WarManager warManager;

    private void Start()
    {
        //warManager = FindObjectOfType<WarManager>();
        // CanvasRect = warManager.Canvas.GetComponent<RectTransform>();
        // UI_Element = warManager.Canvas.GetComponent<RectTransform>();
        levelText = transform.parent.GetComponent<NPC>().levelText.text;
    }
    void Update()
    {
        if (levelText)
        {
            Vector3 levelPos = Camera.main.WorldToScreenPoint(this.transform.position); // change label to screen space
            // Vector3 WorldObject_ScreenPosition = new Vector3(((levelPos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)), ((levelPos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
            // UI_Element.anchoredPosition = WorldObject_ScreenPosition;
            levelText.transform.position = levelPos;
        }
    }
}
