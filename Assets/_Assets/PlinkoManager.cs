using System.Collections.Generic;
using UnityEngine;

public class PlinkoManager : MonoBehaviour
{
    [SerializeField] private int maxSpecialPinHitCount;
    [SerializeField] private List<SpawnerView> spawnerViews;
    [SerializeField] private List<SpawnButtonView> spawnerButtonViews;
    [SerializeField] private List<SpecialPinView> specialPinViews;
    [SerializeField] private BallScoreView ballScoreView;
    [SerializeField] private TotalScoreView totalScoreView;

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
    }

    public void OnBallHitSpecialPin()
    {
        print("Special pin hit");

        currentSpecialPinHitCount++;

        foreach (var specialPinView in specialPinViews)
        {
            specialPinView.SetFillAmount(currentSpecialPinHitCount, maxSpecialPinHitCount);
        }
        
        ballScoreView.SetScoreText(currentSpecialPinHitCount);
    }

    public void OnSpawnButtonClicked(int index)
    {
        print("Spawn button clicked");
        DisallowAllSpawnerButtonInteractions(index);
    }

    public void OnBallEnteredBasket(int multiplier)
    {
        print("Ball entered");
        AllowAllSpawnerButtonInteractions();

        
        foreach (var specialPinView in specialPinViews)
        {
            specialPinView.SetFillAmount(0, currentSpecialPinHitCount);
        }
        
        ballScoreView.SetScoreText(currentSpecialPinHitCount);
        ballScoreView.ResetWithAnimation();
        totalScoreView.AddScoreWithAnimation(multiplier * currentSpecialPinHitCount);
        
        currentSpecialPinHitCount = 0;
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