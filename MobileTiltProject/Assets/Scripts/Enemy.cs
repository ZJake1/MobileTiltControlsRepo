using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;

    GameObject projectiles;

    public int health = 1;

    GameObject enemy;
    Rigidbody2D rb;

    GameObject player;

    float moveSpeed = 250f;

    float attackCooldown = 3f;

    Vector3 startSize;
    float sizeMulti = 0f;

    // Called right before update
    private void Start() // Declare variables
    {
        health = 1;
        
        enemy = gameObject;
        player = GameObject.FindWithTag("Player");
        projectiles = GameObject.FindWithTag("Projectiles");
        rb = enemy.GetComponent<Rigidbody2D>();

        startSize = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        if (health > 0)
        {
            Move(); // Move the enemy
            Attack(); // Fires a projectile
            Rotate(player.transform.position); // Rotate the enemy
        }
        else
        {
            if (transform.localScale.magnitude > startSize.magnitude * 2)
                Destroy(gameObject);

            transform.localScale += startSize * sizeMulti;
            sizeMulti += Time.deltaTime;
        }
    }

    void Move() // Move the enemy towards its forward vector
    {
        if ((enemy.transform.position - player.transform.position).magnitude > 8) // Check if the enemy is far enough from the player to move closer
        {
            rb.AddForce(-enemy.transform.right * moveSpeed * Time.deltaTime); // Moves the enemy towards the forward side of the sprite
        }
        else if ((enemy.transform.position - player.transform.position).magnitude < 6)
        {
            rb.AddForce(enemy.transform.right * moveSpeed * Time.deltaTime); // Moves the enemy away from the forward side of the sprite
        }
    }

    void Attack() // Makes the enemy shoot a bullet infront of it
    {
        attackCooldown -= Time.deltaTime; // Negates deltaTime from the attack cooldown value
        if (attackCooldown <= 0) // Checks if the attack is off cooldown
        {
            attackCooldown = Random.Range(4, 8); // Sets a new attack cooldown randomly between the given values
            Instantiate(projectile, enemy.transform.position, enemy.transform.rotation, projectiles.transform); // Instantiates the bullet at the right position and rotation
        }
    }

    void Rotate(Vector3 pos) // Rotate the enemy towards the given position
    {
        Vector3 dif = enemy.transform.position - pos;
        float zRot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0, 0, zRot);
    }
}
