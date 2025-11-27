using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallView : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;

    private bool collidedWithBasket;

    public event Action<SpecialPinView> SpecialPinHit;
    public event Action<BasketView> BasketEntered;

    public void Initialize()
    {
            rigidbody = GetComponent<Rigidbody2D>();

        collidedWithBasket = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out SpecialPinView specialPin))
        {
            if (!specialPin.IsHighlighted)
            {
                float power = UnityEngine.Random.Range(3f, 5f);
                Vector2 contact = other.GetContact(0).point;
                Vector2 direction = ((Vector2)transform.position - contact).normalized;
                rigidbody.AddForce(direction * power, ForceMode2D.Impulse);
                specialPin.GetHit();
                SpecialPinHit?.Invoke(specialPin);
            }

            return;
        }

        if (other.gameObject.TryGetComponent(out BasketView basket))
        {
            if (!collidedWithBasket)
            {
                collidedWithBasket = true;
                BasketEntered?.Invoke(basket);
                Destroy(gameObject);
            }
        }
    }
}
