using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    private Image bar;
    private Color startColor;
    private void Start()
    {
        bar = GetComponent<Image>();
        startColor = bar.color;
    }
    private void Update()
    {
        float ratio = (Time.time - GameManager.Instance.LastChangeTime)  / GameManager.Instance.GoalChangeInterval;
        bar.fillAmount = 1f - ratio;
        bar.color = ratio < 0.8f ? startColor: Color.red;
    }
}
