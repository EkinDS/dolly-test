using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChestBarView : MonoBehaviour
{
    [SerializeField] private float maximumProgress = 100f;
    [SerializeField] private Image fillerImage;
    [SerializeField] private RewardStep[] rewardSteps;

    private PlinkoManager plinkoManager;
    private float progress;
    private bool[] rewardGiven;

    public void Initialize(PlinkoManager newPlinkoManager)
    {
        plinkoManager = newPlinkoManager;
        rewardGiven = new bool[rewardSteps.Length];

        progress = 0F;
        UpdateFill();
    }

    public void AddProgress(float progressToAdd)
    {
        progress += progressToAdd;
        progress = Mathf.Clamp(progress, 0F, maximumProgress);

        UpdateFill();
        CheckRewards();
    }

    private void UpdateFill()
    {
        if (fillerImage != null && maximumProgress > 0f)
        {
            fillerImage.DOFillAmount(progress / maximumProgress, 1F).SetDelay(0.5F);
        }
    }

    private void CheckRewards()
    {
        if (plinkoManager == null || rewardSteps == null) return;

        for (int i = 0; i < rewardSteps.Length; i++)
        {
            if (rewardGiven[i]) 
                continue;

            if (progress >= rewardSteps[i].threshold)
            {
                rewardGiven[i] = true;
                plinkoManager.OnRewardReached(rewardSteps[i].rewardAmount);
            }
        }
    }
    
    [Serializable]
    public class RewardStep
    {
        public float threshold;
        public float rewardAmount;
    }
}