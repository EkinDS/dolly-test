using System.Collections.Generic;

public class PlinkoGamePresenter : ISpawnButtonListener
{
    private readonly PlinkoGameModel model;

    private readonly List<SpawnerView> spawners;
    private readonly List<SpawnButtonView> spawnButtons;
    private readonly List<SpecialPinView> specialPins;

    private readonly IBallScoreView ballScoreView;
    private readonly ITotalScoreView totalScoreView;
    private readonly IChestBarView chestBarView;
    private readonly ICurrencyView currencyView;

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
        this.model = model;
        this.spawners = spawners;
        this.spawnButtons = spawnButtons;
        this.specialPins = specialPins;
        this.ballScoreView = ballScoreView;
        this.totalScoreView = totalScoreView;
        this.chestBarView = chestBarView;
        this.currencyView = currencyView;

        _rewardSteps = this.chestBarView.GetRewardSteps() ?? new RewardStep[0];
        _chestRewardGiven = new bool[_rewardSteps.Length];
    }

    public void Initialize()
    {
        for (int i = 0; i < spawnButtons.Count; i++)
        {
            spawnButtons[i].Initialize(i, this);
        }

        ballScoreView.SetScore(0);
        totalScoreView.SetScore(model.TotalScore);
        currencyView.SetCurrency(model.Currency);
        chestBarView.SetFill(0f);

        for (int i = 0; i < _chestRewardGiven.Length; i++)
        {
            _chestRewardGiven[i] = false;
        }
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
        foreach (var b in spawnButtons)
        {
            b.DisallowInteraction(index);
        }

        if (index < 0 || index >= spawners.Count)
        {
            return;
        }

        SpawnerView spawner = spawners[index];
        BallView ball = spawner.SpawnBall();
        if (ball == null)
            return;

        BindBall(ball);

        model.ResetForNewBall();
        ballScoreView.SetScore(0);
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
        model.RegisterSpecialPinHit();

        foreach (var sp in specialPins)
        {
            sp.SetFillAmount(model.CurrentSpecialPinHitCount, model.MaxSpecialPinHitCount);
        }

        ballScoreView.SetScore(model.CurrentBallScore);
    }

    private void HandleBasketEntered(BasketView basket)
    {
        int multiplier = basket.GetMultiplier();

        model.ResolveBallInBasket(multiplier);

        ballScoreView.SetScore(model.CurrentBallScore);
        ballScoreView.ResetWithAnimation();

        totalScoreView.AddScoreWithAnimation(model.CurrentBallScore);
        chestBarView.SetFill(model.ChestProgress / model.ChestMaxProgress);

        CheckChestRewards();

        foreach (var b in spawnButtons)
        {
            b.AllowInteraction();
        }

        foreach (var sp in specialPins)
        {
            sp.SetFillAmount(0, model.MaxSpecialPinHitCount);
        }
    }

    private void CheckChestRewards()
    {
        float progress = model.ChestProgress;

        for (int i = 0; i < _rewardSteps.Length; i++)
        {
            if (_chestRewardGiven[i])
                continue;

            if (progress >= _rewardSteps[i].threshold)
            {
                _chestRewardGiven[i] = true;

                float rewardAmount = _rewardSteps[i].rewardAmount;
                model.AddCurrency(rewardAmount);

                currencyView.SetCurrencyWithAnimation(model.Currency);
                chestBarView.TriggerReward(i, _rewardSteps[i]);
            }
        }
    }
}
