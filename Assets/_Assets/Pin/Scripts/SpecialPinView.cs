using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialPinView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI plusText;
    [SerializeField] private Image fillerImage;
    [SerializeField] private Image fullImage;

    private PlinkoManager plinkoManager;

    public void Initialize(PlinkoManager newPlinkoManager, int currentHitCount, int maxHitCount)
    {
        plinkoManager = newPlinkoManager;
        
        SetFillAmount(currentHitCount, maxHitCount);
    }

    public bool IsHighlighted { get; set; }

    public void GetHit()
    {
        AnimatePlusText();

        plinkoManager.OnBallHitSpecialPin();
    }
    
    public void SetFillAmount(int currentHitCount, int maxHitCount)
    {
        fillerImage.fillAmount = (float)currentHitCount / maxHitCount;

        if (currentHitCount >= maxHitCount)
        {
            Highlight(maxHitCount);
        }
        else
        {
            Unhighlight();
        }
    }

    private void AnimatePlusText()
    {
        TextMeshProUGUI newPlusText = Instantiate(plusText, plusText.transform.parent);

        newPlusText.gameObject.SetActive(true);
        newPlusText.transform.DOScale(1.25F, 0.2F);
        newPlusText.transform.DOLocalMoveY(60F, 1.1F).OnComplete(() => Destroy(newPlusText.gameObject));
        newPlusText.DOFade(0F, 1F);
    }

    private void Unhighlight()
    {
        IsHighlighted = false;
        multiplierText.text = "5";
        fullImage.gameObject.SetActive(false);
    }

    private void Highlight(int maxHitCount)
    {
        if (IsHighlighted)
        {
            return;
        }

        fullImage.gameObject.SetActive(true);
        multiplierText.text = maxHitCount.ToString();
        IsHighlighted = true;
    }
}