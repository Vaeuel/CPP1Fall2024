using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{

    [SerializeField, Range(1, 50)] private float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime); //Destroys object after period of time set in variable "lifeTime"
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity; //
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check the tag of the object that the projectile collided with
        if (collision.collider.CompareTag("Player") ||
            collision.collider.CompareTag("Collectible") ||
            collision.collider.CompareTag("PowerUp") ||
            collision.collider.CompareTag("Ground"))
        {
            // Do nothing if the projectile hits these objects
            return;
        }
        else
        {
            // Destroy the projectile if it hits anything else
            Destroy(gameObject);
        }
    }
}
