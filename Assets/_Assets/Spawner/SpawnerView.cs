using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private Transform spawnIndicatorTransform;
    [SerializeField] private BallView _ballViewPrefab;

    public void SpawnBall()
    {
        Instantiate(_ballViewPrefab, spawnIndicatorTransform.position, Quaternion.identity, transform);
    }
}