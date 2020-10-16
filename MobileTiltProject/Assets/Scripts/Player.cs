using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        if (Input.GetKey("w"))
        {
            dir = new Vector3(0, dir.y + 0.1f, 0);
        }
        if (Input.GetKey("s"))
        {
            dir = new Vector3(0, dir.y + -0.1f, 0);
        }
        if (Input.GetKey("a"))
        {
            dir = new Vector3(dir.x + -0.1f, 0, 0);
        }
        if (Input.GetKey("d"))
        {
            dir = new Vector3(dir.x + 0.1f, 0, 0);
        }
        // dir = GetDirection(); // Get the direction of the tilted phone
        Move(dir); // Move the player's character
        Resistance(); // Remove velocity from the player to slow them down
    }

    Vector3 GetDirection() // Function to check if the phone is tilted
    {
        Vector3 dir = new Vector3(Input.acceleration.x, Input.acceleration.y, 0);
        return dir;
    }

    void Move(Vector3 dir) // Move the player's character towards the direction the phone was tilted
    {
        print(dir);
        rb.velocity = new Vector3(Mathf.Min(rb.velocity.x + dir.x, 10), Mathf.Min(rb.velocity.y + dir.y, 10), 0);
    }

    void Resistance()
    {
        rb.velocity -= rb.velocity / 180;
    }
}