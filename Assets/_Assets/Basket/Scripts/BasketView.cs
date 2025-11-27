using UnityEngine;

public class BasketView : MonoBehaviour
{
    [SerializeField] private int multiplier;
    [SerializeField] private GameObject twoMultiplier;
    [SerializeField] private GameObject threeMultiplier;
    [SerializeField] private GameObject fiveMultiplier;

    private void Awake()
    {
        twoMultiplier.gameObject.SetActive(multiplier == 2);
        threeMultiplier.gameObject.SetActive(multiplier == 3);
        fiveMultiplier.gameObject.SetActive(multiplier == 5);
    }

    public int GetMultiplier()
    {
        return multiplier;
    }
}