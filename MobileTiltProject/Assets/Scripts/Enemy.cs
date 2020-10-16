using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;

    private GameObject projectiles;

    private GameObject enemy;
    private Rigidbody2D rb;

    private GameObject player;

    private float moveSpeed = 500;

    private float attackCooldown = 5;

    // Called right before update
    private void Start()
    {
        enemy = gameObject;
        player = GameObject.FindWithTag("Player");
        projectiles = GameObject.FindWithTag("Projectiles");
        rb = enemy.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move(); // Move the enemy
        Attack(); // Fires a projectile
        Rotate(player.transform.position); // Rotate the enemy
    }

    void Move() // Move the enemy towards its forward vector
    {
        if ((enemy.transform.position - player.transform.position).magnitude > 8) // Check if the enemy is close enough to stop moving
        {
            rb.AddForce(-enemy.transform.right * moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0)
        {
            attackCooldown = 5;
            Instantiate(projectile, enemy.transform.position, enemy.transform.rotation, projectiles.transform);
        }
    }

    void Rotate(Vector3 pos) // Rotate the enemy towards the given position
    {
        Vector3 dif = enemy.transform.position - pos;
        float zRot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0, 0, zRot);
    }
}
