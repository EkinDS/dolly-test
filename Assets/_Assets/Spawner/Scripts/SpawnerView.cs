using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private BallView ballPrefab;
    [SerializeField] private Transform spawnIndicatorTransform;

    public BallView SpawnBall()
    {
     
        Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), 0f, 0f);
        BallView ball = Instantiate(
            ballPrefab,
            spawnIndicatorTransform.position + offset,
            Quaternion.identity
        );

        ball.Initialize();
        return ball;
    }
}
