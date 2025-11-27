using System.Collections.Generic;
using UnityEngine;

public class PlinkoGamePresenter : ISpawnButtonListener
{
    private readonly PlinkoGameModel _model;

    private readonly List<SpawnerView> _spawners;
    private readonly List<SpawnButtonView> _spawnButtons;
    private readonly List<SpecialPinView> _specialPins;

    private readonly IBallScoreView _ballScoreView;
    private readonly ITotalScoreView _totalScoreView;
    private readonly IChestBarView _chestBarView;
    private readonly ICurrencyView _currencyView;

    private readonly List<BallView> _activeBalls = new List<BallView>();
    private readonly bool[] _chestRewardGiven;
    private readonly RewardStep[] _rewardSteps;

    public PlinkoGamePresenter(
        PlinkoGameModel model,
        List<SpawnerView> spawners,
        List<SpawnButtonView> spawnButtons,
        List<SpecialPinView> specialPins,
        IBallScoreView ballScoreView,
        ITotalScoreView totalScoreView,
        IChestBarView chestBarView,
        ICurrencyView currencyView)
    {
        _model = model;
        _spawners = spawners;
        _spawnButtons = spawnButtons;
        _specialPins = specialPins;
        _ballScoreView = ballScoreView;
        _totalScoreView = totalScoreView;
        _chestBarView = chestBarView;
        _currencyView = currencyView;

        _rewardSteps = _chestBarView.GetRewardSteps() ?? new RewardStep[0];
        _chestRewardGiven = new bool[_rewardSteps.Length];
    }

    public void Initialize()
    {
        for (int i = 0; i < _spawnButtons.Count; i++)
        {
            _spawnButtons[i].Initialize(i, this);
        }

        _ballScoreView.SetScore(0);
        _totalScoreView.SetScore(_model.TotalScore);
        _currencyView.SetCurrency(_model.Currency);
        _chestBarView.SetFill(0f);
        _chestBarView.ResetRewards();
    }

    public void Dispose()
    {
        foreach (var ball in _activeBalls)
        {
            UnbindBall(ball);
        }
        _activeBalls.Clear();
    }

    public void OnSpawnButtonClicked(int index)
    {
        foreach (var b in _spawnButtons)
        {
            b.DisallowInteraction(index);
        }

        if (index < 0 || index >= _spawners.Count)
        {
            return;
        }

        SpawnerView spawner = _spawners[index];
        BallView ball = spawner.SpawnBall();
        if (ball == null)
            return;

        BindBall(ball);

        _model.ResetForNewBall();
        _ballScoreView.SetScore(0);
    }

    private void BindBall(BallView ball)
    {
        _activeBalls.Add(ball);
        ball.SpecialPinHit += HandleSpecialPinHit;
        ball.BasketEntered += HandleBasketEntered;
    }

    private void UnbindBall(BallView ball)
    {
        ball.SpecialPinHit -= HandleSpecialPinHit;
        ball.BasketEntered -= HandleBasketEntered;
    }

    private void HandleSpecialPinHit(SpecialPinView pin)
    {

        _model.RegisterSpecialPinHit();

        foreach (var sp in _specialPins)
        {
            sp.SetFillAmount(_model.CurrentSpecialPinHitCount, _model.MaxSpecialPinHitCount);
        }

        _ballScoreView.SetScore(_model.CurrentBallScore);

    }

    private void HandleBasketEntered(BasketView basket)
    {
        int multiplier = basket.GetMultiplier();

        _model.ResolveBallInBasket(multiplier);


        _ballScoreView.SetScore(_model.CurrentBallScore);
        _ballScoreView.ResetWithAnimation();

        _totalScoreView.AddScoreWithAnimation(_model.CurrentBallScore);
        _chestBarView.SetFill(_model.ChestProgress / _model.ChestMaxProgress);

        CheckChestRewards();

        foreach (var b in _spawnButtons)
        {
            b.AllowInteraction();
        }
        
        foreach (var sp in _specialPins)
        {
            sp.SetFillAmount(0, _model.MaxSpecialPinHitCount);
        }

    }

    private void CheckChestRewards()
    {

        float progress = _model.ChestProgress;

        for (int i = 0; i < _rewardSteps.Length; i++)
        {
            if (progress >= _rewardSteps[i].threshold)
            {
                _chestRewardGiven[i] = true;

                float rewardAmount = _rewardSteps[i].rewardAmount;
                _model.AddCurrency(rewardAmount);

                _currencyView.SetCurrencyWithAnimation(_model.Currency);
                _chestBarView.TriggerReward(i, _rewardSteps[i]);
            }
        }
    }
}
