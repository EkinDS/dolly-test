using DG.Tweening;
using TMPro;
using UnityEngine;

public class TotalScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;

    public void AddScoreWithAnimation(int scoreToAdd)
    {
        int startValue = score;
        int targetValue = score + scoreToAdd;

        score += scoreToAdd;

        DOTween.To(() => startValue, x =>
        {
            startValue = x;
            scoreText.text = x.ToString();
        }, targetValue, 0.5f).SetDelay(0.5F);
    }
}