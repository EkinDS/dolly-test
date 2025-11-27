using UnityEngine;
using Random = UnityEngine.Random;

public class BallView : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private PlinkoManager plinkoManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(PlinkoManager newPlinkoManager)
    {
        plinkoManager = newPlinkoManager;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SpecialPinView specialPinView = other.gameObject.GetComponent<SpecialPinView>();
        
        if (specialPinView)
        {
            if (!specialPinView.IsHighlighted)
            {
                float power = Random.Range(3F, 5F);
                Vector3 currentPosition = transform.position;
                Vector3 otherPosition = other.transform.position;
                Vector3 direction = currentPosition - otherPosition;
                Vector3 normalizedDirection = direction.normalized;
                Vector3 force = power * normalizedDirection;

                _rigidbody.AddForce(force, ForceMode2D.Impulse);

                specialPinView.GetHit();
            }

            return;
        }

        BasketView basketView = other.gameObject.GetComponent<BasketView>();
        
        if (basketView)
        {
            plinkoManager.OnBallEnteredBasket(basketView.GetMultiplier());
            
            Destroy(gameObject);

            return;
        }
    }
}