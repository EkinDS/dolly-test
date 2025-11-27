public interface IChestBarView
{
    RewardStep[] GetRewardSteps();
    void SetFill(float normalized);
    void TriggerReward(int index, RewardStep step);
    void ResetRewards();
}
