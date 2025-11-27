using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialPinView : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Image fullImage;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI plusText;

    public bool IsHighlighted { get; private set; }

    private void Awake()
    {
       
            plusText.gameObject.SetActive(false);
        

        SetFillAmount(0, 1);
    }

    public void SetFillAmount(int currentHits, int maxHits)
    {
        float normalized = maxHits > 0 ? (float)currentHits / maxHits : 0f;

        if (fillImage != null)
        {
            fillImage.fillAmount = normalized;
        }

        bool shouldHighlight = normalized >= 1f;
        if (shouldHighlight && !IsHighlighted)
        {
            fullImage.gameObject.SetActive(true);
            Highlight();
            multiplierText.text = "50";
        }
        else
        {
            fullImage.gameObject.SetActive(false);
            multiplierText.text = "5";

        }

        IsHighlighted = shouldHighlight;
    }

    private void Highlight()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 1, 0.5f);
    }

    public void GetHit()
    {
        AnimatePlusText();
    }

    private void AnimatePlusText()
    {
        TextMeshProUGUI newPlusText = Instantiate(plusText, plusText.transform.parent);

        newPlusText.gameObject.SetActive(true);
        newPlusText.transform.localScale = Vector3.one;

        newPlusText.transform.DOScale(1.25f, 0.2f);
        newPlusText.transform
            .DOLocalMoveY(newPlusText.transform.localPosition.y + 60f, 1.1f)
            .OnComplete(() => Destroy(newPlusText.gameObject));
        newPlusText.DOFade(0f, 1f);
    }
}
