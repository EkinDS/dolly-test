using DG.Tweening;
using TMPro;
using UnityEngine;

public class BallScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;
    private Tween tween;

    public void SetScoreText(int newScore)
    {
        score = newScore;
        scoreText.text = score.ToString();
    }

    public void ResetWithAnimation()
    {
        int startValue = score;

        tween.Kill();
        tween = DOTween.To(() => startValue, x =>
        {
            startValue = x;
            scoreText.text = x.ToString();
        }, 0, 0.5f).SetDelay(0.5F).OnComplete(() => { score = 0; });
    }
}