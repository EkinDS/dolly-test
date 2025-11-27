using DG.Tweening;
using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour, ICurrencyView
{
    [SerializeField] private TextMeshProUGUI currencyText;

    private float currency;
    private Tween tween;

    public void SetCurrency(float value)
    {
        currency = value;

        currencyText.text = "$" + currency.ToString("0.00");
    }

    public void SetCurrencyWithAnimation(float value)
    {
        float startValue = currency;
        float targetValue = value;

        tween = DOTween.To(
            () => startValue,
            x =>
            {
                startValue = x;
                if (currencyText != null)
                {
                    currencyText.text = "$" + startValue.ToString("0.00");
                }
            },
            targetValue,
            0.5f
        ).SetDelay(0.5f).OnComplete(() => { currency = targetValue; });
    }
}