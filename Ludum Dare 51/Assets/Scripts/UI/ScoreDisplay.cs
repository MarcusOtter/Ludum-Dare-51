using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private Color _textDefaultColor, _textScoreColor;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        GameManager.OnScore += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.OnScore -= UpdateScore;
    }

    private void UpdateScore()
    {
        text.text = GameManager.GetPoints().ToString();
        transform.localScale = Vector3.one * 2;
        text.color = _textScoreColor;
    }
    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 6f * Time.deltaTime);
        text.color = Color.Lerp(text.color, _textDefaultColor, 6f * Time.deltaTime);
    }
}
