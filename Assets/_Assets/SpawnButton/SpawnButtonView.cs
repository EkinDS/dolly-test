using UnityEngine;

public class SpawnButtonView : MonoBehaviour
{
    [SerializeField]  private SpawnerView spawnerView;
    
    public void OnSpawnButtonClicked()
    {
        spawnerView.SpawnBall();
    }
}
