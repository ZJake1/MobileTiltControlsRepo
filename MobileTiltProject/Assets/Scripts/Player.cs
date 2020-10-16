using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    private bool onMobile = false;

    public int health = 10;

    private float moveSpeed = 50;
    private float maxSpeed = 2500;

    private Vector3 dir = new Vector3(0, 0, 0);

    private void Start()
    {
        player = gameObject;
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        ChangeDirection(); // Change the movement direction for the move function
        Move(dir); // Move the character
        Rotate((player.transform.position + new Vector3(rb.velocity.x, rb.velocity.y, 0))); // Rotate the character
    }

    void ChangeDirection()
    {
        dir = new Vector3(0, 0, 0);
        if (!onMobile) // If on desktop change the dir variable to the direction pressed
        {
            if (Input.GetKey("w"))
            {
                dir = new Vector3(dir.x, dir.y + (moveSpeed * Time.deltaTime), 0);
            }
            if (Input.GetKey("s"))
            {
                dir = new Vector3(dir.x, dir.y + (-moveSpeed * Time.deltaTime), 0);
            }
            if (Input.GetKey("a"))
            {
                dir = new Vector3(dir.x + (-moveSpeed * Time.deltaTime), dir.y, 0);
            }
            if (Input.GetKey("d"))
            {
                dir = new Vector3(dir.x + (moveSpeed * Time.deltaTime), dir.y, 0);
            }
        }
        else // If on mobile then get the direction relative to the phone's tilt direction
        {
            dir = GetDirection();
        }
    }

    Vector3 GetDirection() // Find and return the direction of the tilted phone
    {
        Vector3 dir = new Vector3(Input.acceleration.x, Input.acceleration.y, 0);
        return dir;
    }

    void Move(Vector3 dir) // Move the player's character towards the direction the phone was tilted
    {
        rb.velocity = new Vector3(Mathf.Min(rb.velocity.x + dir.x, maxSpeed), Mathf.Min(rb.velocity.y + dir.y, maxSpeed), 0);
    }

    void Rotate(Vector3 pos) // Rotate the player towards the given position
    {
        Vector3 dif = -(player.transform.position - pos);
        float zRot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0, 0, zRot);
    }
}
