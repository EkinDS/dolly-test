using DG.Tweening;
using TMPro;
using UnityEngine;

public class TotalScoreView : MonoBehaviour, ITotalScoreView
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;

    public void SetScore(int newScore)
    {
        this.score = newScore;
        if (scoreText != null)
        {
            scoreText.text = this.score.ToString();
        }
    }

    public void AddScoreWithAnimation(int scoreToAdd)
    {
        int startValue = score;
        int targetValue = score + scoreToAdd;


       DOTween.To(
            () => startValue,
            x =>
            {
                startValue = x;
                if (scoreText != null)
                {
                    scoreText.text = startValue.ToString();
                }
            },
            targetValue,
            0.5f
        ).OnComplete(() =>
        {
            score = targetValue;
        });
    }
}
