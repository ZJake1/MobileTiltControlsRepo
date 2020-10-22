using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    GameObject player;

    int speed = 3;

    float maxTimer = 8f;
    float timer;

    float rotSpeed = 3f;

    Vector3 startSize;
    float sizeMulti = 0f;

    // Fires right before Update()
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        timer = maxTimer;
        startSize = transform.localScale;
    }

    // Called every frame
    private void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 180));
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
        if (timer <= 0) // Checks if the bullets up timer has reached 0 and destroys it if this is the case
        {
            if (transform.localScale.magnitude > startSize.magnitude * 2)
                Destroy(gameObject);

            transform.localScale += startSize * sizeMulti;
            sizeMulti += Time.deltaTime;
        }
        timer -= Time.deltaTime; // Negates deltaTime from the uptime timer
        rb.velocity = -transform.right * speed; // sets the bullets velocity to be to the front of the sprite
    }

    private void OnTriggerEnter2D(Collider2D col) // Checks for collisions
    {
        if (col.tag == "Player" && timer > 0) // Checks if the hit object has the "Player" tag
        {
            col.GetComponent<Player>().health--; // Negates 1 hp from the player
            timer = 0;
        }
        else if (col.tag == "Barrel")
        {
            Destroy(col.gameObject);
        }
    }
}
