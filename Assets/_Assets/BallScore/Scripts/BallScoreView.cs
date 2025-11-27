using DG.Tweening;
using TMPro;
using UnityEngine;

public class BallScoreView : MonoBehaviour, IBallScoreView
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

    public void ResetWithAnimation()
    {
        DOTween.To(() => score, x =>
        {
            score = x;
            if (scoreText != null)
            {
                scoreText.text = score.ToString();
            }
        }, 0, 0.5f).SetDelay(0.5f);
    }
}