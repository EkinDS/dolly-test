using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallView : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<SpecialPinView>())
        {
            float power = Random.Range(2F, 4F);
            Vector3 currentPosition = transform.position;
            Vector3 otherPosition = other.transform.position;
            Vector3 direction = currentPosition - otherPosition;
            Vector3 normalizedDirection = direction.normalized;
            Vector3 force = power * normalizedDirection;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        if (other.gameObject.GetComponent<BasketView>())
        {
            print("des");
            Destroy(gameObject);
        }
    }
}