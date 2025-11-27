public class PlinkoGameModel
{
    public int MaxSpecialPinHitCount { get; }
    public int ScorePerHit { get; }

    public int CurrentSpecialPinHitCount { get; private set; }
    public int CurrentBallScore { get; private set; }
    public int TotalScore { get; private set; }

    public float ChestProgress { get; private set; }
    public float ChestMaxProgress { get; }

    public float Currency { get; private set; }

    public PlinkoGameModel(int maxSpecialPinHitCount, int scorePerHit, float chestMaxProgress)
    {
        MaxSpecialPinHitCount = maxSpecialPinHitCount;
        ScorePerHit = scorePerHit;
        ChestMaxProgress = chestMaxProgress;

        CurrentSpecialPinHitCount = 0;
        CurrentBallScore = 0;
        TotalScore = 0;
        ChestProgress = 0f;
        Currency = 0f;
    }

    public void ResetForNewBall()
    {
        CurrentSpecialPinHitCount = 0;
        CurrentBallScore = 0;
    }

    public void RegisterSpecialPinHit()
    {
        CurrentSpecialPinHitCount++;

        if (CurrentSpecialPinHitCount >= MaxSpecialPinHitCount)
        {
            CurrentBallScore = MaxSpecialPinHitCount * ScorePerHit * 2;
        }
        else
        {
            CurrentBallScore = CurrentSpecialPinHitCount * ScorePerHit;
        }
    }

    public void ResolveBallInBasket(int multiplier)
    {
        CurrentBallScore *= multiplier;
        TotalScore += CurrentBallScore;

        AddChestProgress(CurrentBallScore);
    }

    public void AddChestProgress(int amount)
    {
        ChestProgress += amount;
        if (ChestProgress > ChestMaxProgress)
        {
            ChestProgress = ChestMaxProgress;
        }
    }

    public void AddCurrency(float amount)
    {
        Currency += amount;
    }
}
