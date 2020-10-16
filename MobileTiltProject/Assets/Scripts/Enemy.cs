using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private Rigidbody2D rb;

    private float moveSpeed = 500;

    private void Start()
    {
        rb = enemy.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move(); // Move the enemy
        Rotate(player.transform.position); // Rotate the enemy
    }

    void Move() // Move the enemy forward
    {
        print((enemy.transform.position - player.transform.position).magnitude);
        if ((enemy.transform.position - player.transform.position).magnitude > 10)
        {
            rb.AddForce(-enemy.transform.right * moveSpeed * Time.deltaTime);
        }
    }

    void Rotate(Vector3 pos) // Rotate the enemy towards the given position
    {
        Vector3 dif = enemy.transform.position - pos;
        float zRot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0, 0, zRot);
    }
}
