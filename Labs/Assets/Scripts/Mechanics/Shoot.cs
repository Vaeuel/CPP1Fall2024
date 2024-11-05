using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    SpriteRenderer sr;
    public Vector2 initialShotVelocity;

    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initialShotVelocity == Vector2.zero)
        {
            Debug.Log("Initial shot velocity is Zero, Changing to default value");
            initialShotVelocity.x = 4f;
            initialShotVelocity.y = 4f;
        }
        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)

            Debug.Log("Set default values on shoot script");

        if (!spawnPointRight || !spawnPointLeft)
        {
            Debug.Log("No Projectile spawn set. creating ones assuming spawn is .55 units up and .35 off center");

            // ground check initialization
            GameObject newGameObject = new GameObject(); // creates new game object in scene and names it 
            newGameObject.transform.SetParent(transform); // childs the new game object under what ever uses this script
            newGameObject.transform.localPosition = new Vector3(.35f,.55f,0); // Zeros the new object location local to its' parent
            newGameObject.name = "SpawnPointRight"; // renames the game object in for the hierarchy
            spawnPointRight = newGameObject.transform; // Sets and returns the ground check objects trans values to global variable.

            GameObject newGameObject2 = new GameObject();
            newGameObject2.transform.SetParent(transform);
            newGameObject2.transform.localPosition = new Vector3(-.35f, .55f, 0);
            newGameObject2.name = "SpawnPointLeft";
            spawnPointLeft = newGameObject2.transform;

        }

    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.SetVelocity(initialShotVelocity);
        }

        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.SetVelocity(new Vector2(-initialShotVelocity.x, initialShotVelocity.y));
        }
    }
}
