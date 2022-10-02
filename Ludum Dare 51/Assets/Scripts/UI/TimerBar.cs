using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerBar : MonoBehaviour
{
    private Image bar;
    private void Start()
    {
        bar = GetComponent<Image>();
    }
    private void Update()
    {
        float ratio = (Time.time - GameManager.Instance.LastChangeTime)  / GameManager.Instance.GoalChangeInterval;
        bar.fillAmount = 1f - ratio;
        bar.color = ratio < 0.8f ? Color.white : Color.red;
    }
}
