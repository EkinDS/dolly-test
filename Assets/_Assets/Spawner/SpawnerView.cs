using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private Transform spawnIndicatorTransform;
    [SerializeField] private BallView _ballViewPrefab;

    private PlinkoManager plinkoManager;

    public void Initialize(PlinkoManager newPlinkoManager)
    {
        plinkoManager = newPlinkoManager;
    }

    public void SpawnBall()
    {
        BallView newBallView = Instantiate(_ballViewPrefab,
            spawnIndicatorTransform.position + new Vector3(Random.Range(-0.1F, 0.1F), 0F, 0F), Quaternion.identity,
            transform);

        newBallView.Initialize(plinkoManager);
    }
}