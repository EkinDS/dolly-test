using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonView : MonoBehaviour
{
    [SerializeField] private SpawnerView spawnerView;

    private PlinkoManager plinkoManager;
    private Button button;

    private float initialPositionY;
    private int index;

    private void Awake()
    {
        button = GetComponent<Button>();
        initialPositionY = transform.localPosition.y;
    }

    public void Initialize(PlinkoManager newPlinkoManager, int newIndex)
    {
        plinkoManager = newPlinkoManager;
        index = newIndex;
    }

    public void AllowInteraction()
    {
        button.interactable = true;

        button.transform.DOLocalMoveY(initialPositionY, 0.2F);
    }

    public void DisallowInteraction(int clickedIndex)
    {
        button.interactable = false;

        if (clickedIndex == index)
        {
            button.transform.DOLocalMoveY(initialPositionY - 20F, 0.1F);
        }
        else
        {
            button.transform.DOLocalMoveY(initialPositionY - 20F, 0.1F).SetDelay(0.1F);

        }
    }

    public void OnSpawnButtonClicked()
    {
        spawnerView.SpawnBall();

        plinkoManager.OnSpawnButtonClicked(index);
    }
}