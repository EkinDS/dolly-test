using DG.Tweening;
using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    [SerializeField]  private TextMeshProUGUI currencyText;
    
    private float currency;

    private void Awake()
    {
        currencyText.text = "$0";
    }
    
    public void AddCurrencyWithAnimation(float currencyToAdd)
    {
        float startValue = currency;
        float targetValue = currency + currencyToAdd;

        currency += currencyToAdd;

        DOTween.To(() => startValue, x =>
        {
            startValue = x;
            currencyText.text = "$" + x.ToString("0.00");
        }, targetValue, 0.5f).SetDelay(0.5F);
    }
}
