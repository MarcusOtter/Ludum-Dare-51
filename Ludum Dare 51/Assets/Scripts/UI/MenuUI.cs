using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text latestScoreText;
    [SerializeField] private Transform ground;
    [SerializeField] private float groundYPositionMin = -115f;
    [SerializeField] private float groundYPositionMax = -85f;
    [SerializeField] private float groundRotationMin = -45f;
    [SerializeField] private float groundRotationMax = 135f;

    private void Start()
    {
        highScoreText.text = GameManager.BestScore >= 0 ? "Best score: " + GameManager.BestScore : "";
        latestScoreText.text = GameManager.PreviousScore >= 0 ? "Latest score: " + GameManager.PreviousScore : "";
    }
    
    public void SetVolume(float sliderValue)
    {
        ground.position = ground.position.With(y: Mathf.Lerp(groundYPositionMin, groundYPositionMax, sliderValue));
        ground.rotation = Quaternion.Euler(0, Mathf.Lerp(groundRotationMin, groundRotationMax, sliderValue), 0);

        SoundEffectPlayer.MainVolume = sliderValue;
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }
}
