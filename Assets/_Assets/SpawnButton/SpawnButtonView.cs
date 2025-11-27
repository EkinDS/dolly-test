using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonView : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private Button button;
    [SerializeField] private SpawnerView spawnerView;

    private float _initialPositionY;
    private ISpawnButtonListener _listener;

    public SpawnerView SpawnerView => spawnerView;

    public void Initialize(int newIndex, ISpawnButtonListener listener)
    {
        index = newIndex;
        _listener = listener;


        button = GetComponent<Button>();


        _initialPositionY = button.transform.localPosition.y;
        button.onClick.AddListener(HandleClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        _listener?.OnSpawnButtonClicked(index);
    }

    public void AllowInteraction()
    {
        button.interactable = true;
        button.transform.DOLocalMoveY(_initialPositionY, 0.1f);
    }

    public void DisallowInteraction(int clickedIndex)
    {
        button.interactable = false;

        float targetY = _initialPositionY - 20f;

        if (clickedIndex == index)
        {
            button.transform.DOLocalMoveY(targetY, 0.1f);
        }
        else
        {
            button.transform.DOLocalMoveY(targetY, 0.1f).SetDelay(0.1f);
        }
    }
}