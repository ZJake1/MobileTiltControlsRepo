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
        if (timer <= 0)
        {
            Destroy(bullet);
        }
        timer -= Time.deltaTime;
        rb.velocity = -bullet.transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && timer > 0)
        {
            col.GetComponent<Player>().health--;
            Destroy(bullet);
        }
    }
}
