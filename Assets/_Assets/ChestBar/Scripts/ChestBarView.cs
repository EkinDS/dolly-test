using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChestBarView : MonoBehaviour, IChestBarView
{
    [SerializeField] private float maximumProgress;
    [SerializeField] private Image fillerImage;
    [SerializeField] private RewardStep[] rewardSteps;

    public float MaximumProgress => maximumProgress;
    private Tween fillTween;
    private bool[] rewardGiven;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (rewardSteps == null)
        {
            rewardSteps = new RewardStep[0];
        }

        rewardGiven = new bool[rewardSteps.Length];
        SetFill(0f);
    }

    public RewardStep[] GetRewardSteps()
    {
        return rewardSteps;
    }

    public void SetFill(float normalized)
    {
        normalized = Mathf.Clamp01(normalized);

        fillTween = fillerImage.DOFillAmount(normalized, 1f).SetDelay(0.5f);
    }

    public void TriggerReward(int index, RewardStep step)
    {
        step.rewardObjectTransform.DOScale(1.3F, 0.2F).OnComplete((() =>
        {
            step.rewardObjectTransform.DOScale(1F, 0.2F).SetDelay(0.2F);
        }));
    }

    public void ResetRewards()
    {
        if (rewardGiven == null || rewardGiven.Length != rewardSteps.Length)
        {
            rewardGiven = new bool[rewardSteps.Length];
        }

        for (int i = 0; i < rewardGiven.Length; i++)
        {
            rewardGiven[i] = false;
        }
    }
}
