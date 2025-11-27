using System.Collections.Generic;
using UnityEngine;

public class PlinkoManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int maxSpecialPinHitCount = 5;
    [SerializeField] private int scorePerHit = 5;
    [SerializeField] private float chestMaxProgress;

    [Header("Views")]
    [SerializeField] private List<SpawnerView> spawnerViews;
    [SerializeField] private List<SpawnButtonView> spawnButtonViews;
    [SerializeField] private List<SpecialPinView> specialPinViews;
    [SerializeField] private BallScoreView ballScoreView;
    [SerializeField] private TotalScoreView totalScoreView;
    [SerializeField] private ChestBarView chestBarView;
    [SerializeField] private CurrencyView currencyView;

    private PlinkoGameModel _model;
    private PlinkoGamePresenter _presenter;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        _model = new PlinkoGameModel(
            maxSpecialPinHitCount,
            scorePerHit,
            chestMaxProgress
        );

        if (chestBarView != null)
        {
            chestBarView.Initialize();
        }

        _presenter = new PlinkoGamePresenter(
            _model,
            spawnerViews,
            spawnButtonViews,
            specialPinViews,
            ballScoreView,
            totalScoreView,
            chestBarView,
            currencyView
        );

        _presenter.Initialize();
    }

    private void OnDestroy()
    {
        if (_presenter != null)
        {
            _presenter.Dispose();
        }
    }
}
