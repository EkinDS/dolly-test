using System.Collections.Generic;
using UnityEngine;

public class PlinkoManager : MonoBehaviour
{
    [SerializeField] private int maxSpecialPinHitCount;
    [SerializeField] private int scorePerHit;
    [SerializeField] private List<SpawnerView> spawnerViews;
    [SerializeField] private List<SpawnButtonView> spawnerButtonViews;
    [SerializeField] private List<SpecialPinView> specialPinViews;
    [SerializeField] private BallScoreView ballScoreView;
    [SerializeField] private TotalScoreView totalScoreView;
    [SerializeField] private ChestBarView chestBarView;
    [SerializeField] private CurrencyView currencyView;

    private int currentSpecialPinHitCount;


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (var spawnerView in spawnerViews)
        {
            spawnerView.Initialize(this);
        }

        for (var i = 0; i < spawnerButtonViews.Count; i++)
        {
            var spawnerButtonView = spawnerButtonViews[i];
            spawnerButtonView.Initialize(this, i);
        }

        foreach (var specialPinView in specialPinViews)
        {
            specialPinView.Initialize(this, currentSpecialPinHitCount, maxSpecialPinHitCount);
        }

        chestBarView.Initialize(this);
    }

    public void OnBallHitSpecialPin()
    {
        currentSpecialPinHitCount++;

        foreach (var specialPinView in specialPinViews)
        {
            specialPinView.SetFillAmount(currentSpecialPinHitCount, maxSpecialPinHitCount);
        }

        if (currentSpecialPinHitCount >= maxSpecialPinHitCount)
        {
            ballScoreView.SetScoreText(currentSpecialPinHitCount * scorePerHit * 2);
        }
        else
        {
            ballScoreView.SetScoreText(currentSpecialPinHitCount * scorePerHit);
        }
    }

    public void OnSpawnButtonClicked(int index)
    {
        DisallowAllSpawnerButtonInteractions(index);
    }

    public void OnBallEnteredBasket(int multiplier)
    {
        AllowAllSpawnerButtonInteractions();

        foreach (var specialPinView in specialPinViews)
        {
            specialPinView.SetFillAmount(0, maxSpecialPinHitCount);
        }

        ballScoreView.SetScoreText(currentSpecialPinHitCount * scorePerHit);
        ballScoreView.ResetWithAnimation();
        totalScoreView.AddScoreWithAnimation(multiplier * currentSpecialPinHitCount * scorePerHit);
        chestBarView.AddProgress(multiplier * currentSpecialPinHitCount * scorePerHit);

        currentSpecialPinHitCount = 0;
    }

    public void OnRewardReached(float value)
    {
        currencyView.AddCurrencyWithAnimation(value);
    }

    private void AllowAllSpawnerButtonInteractions()
    {
        foreach (var spawnButtonView in spawnerButtonViews)
        {
            spawnButtonView.AllowInteraction();
        }
    }

    private void DisallowAllSpawnerButtonInteractions(int index)
    {
        foreach (var spawnButtonView in spawnerButtonViews)
        {
            spawnButtonView.DisallowInteraction(index);
        }
    }
}