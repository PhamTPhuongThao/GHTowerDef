using UnityEngine;
using UnityEngine.UI;


public class CountdownStart : MonoBehaviour
{
    Text text;
    private float maxWaitingTime = 4f;
    private float counter;

    void Start()
    {
        text = GetComponent<Text>();
        counter = maxWaitingTime;
        text.text = maxWaitingTime + "";
    }

    void Update()
    {
        text.text = Mathf.FloorToInt(counter % 60) + "";
        counter -= 1 * Time.deltaTime;
        if (counter <= 1)
        {
            this.transform.parent.gameObject.SetActive(false);

        }
    }
}
