using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject bullet;
    private Rigidbody2D rb;
    private CapsuleCollider2D bc;

    private int speed = 6;

    private float timer = 15;

    // Fires right before Update()
    private void Start()
    {
        bullet = gameObject;
        rb = bullet.GetComponent<Rigidbody2D>();
    }

    // Called every frame
    private void Update()
    {
        if (timer <= 0) // Checks if the bullets up timer has reached 0 and destroys it if this is the case
        {
            Destroy(bullet);
        }
        timer -= Time.deltaTime; // Negates deltaTime from the uptime timer
        rb.velocity = -bullet.transform.right * speed; // sets the bullets velocity to be to the front of the sprite
    }

    private void OnTriggerEnter2D(Collider2D col) // Checks for collisions
    {
        if (col.tag == "Player" && timer > 0) // Checks if the hit object has the "Player" tag
        {
            col.GetComponent<Player>().health--; // Negates 1 hp from the player
            Destroy(bullet);
        }
    }
}
