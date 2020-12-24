using UnityEngine;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour
{
    public Text levelLabel;

    private void Start()
    {
    }
    void Update()
    {
        Vector3 levelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        levelLabel.transform.position = levelPos;
    }
}
