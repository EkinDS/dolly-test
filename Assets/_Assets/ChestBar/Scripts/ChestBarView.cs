using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChestBarView : MonoBehaviour, IChestBarView
{
    [SerializeField] private float maximumProgress;
    [SerializeField] private Image fillerImage;
    [SerializeField] private RewardStep[] rewardSteps;

    public float MaximumProgress => maximumProgress;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        SetFill(0f);
    }

    public RewardStep[] GetRewardSteps()
    {
        return rewardSteps;
    }

    public void SetFill(float normalized)
    {
        if (fillerImage == null)
            return;

        normalized = Mathf.Clamp01(normalized);

        fillerImage
            .DOFillAmount(normalized, 1f)
            .SetDelay(0.5f);
    }

    public void TriggerReward(int index, RewardStep step)
    {
        if (step.rewardObjectTransform == null)
            return;

        step.rewardObjectTransform
            .DOScale(1.3f, 0.2f)
            .OnComplete(() =>
            {
                step.rewardObjectTransform
                    .DOScale(1f, 0.2f)
                    .SetDelay(0.2f);
            });
    }
}