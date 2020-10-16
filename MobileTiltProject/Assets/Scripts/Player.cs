using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    private bool onMobile = false;

    public float health = 10;

    public GameObject hearts;

    public Sprite fullHeart;
    public Sprite halfHeart;

    private float moveSpeed = 50;
    private float maxSpeed = 2500;

    private Vector3 dir = new Vector3(0, 0, 0);

    // Called right before update
    private void Start() // Declare variables
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
        UpdateUI();
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
            dir = new Vector3(GetDirection().x * moveSpeed * Time.deltaTime, GetDirection().y * moveSpeed * Time.deltaTime, 0); // Set dir's x and y value to the Input.acceleration of the phone
        }
    }

    Vector3 GetDirection() // Find and return the direction of the tilted phone
    {
        Vector3 newDir = Input.acceleration;
        return newDir;
    }

    void Move(Vector3 dir) // Move the player's character towards the direction the phone was tilted
    {
        rb.velocity = new Vector3(Mathf.Min(rb.velocity.x + dir.x, maxSpeed), Mathf.Min(rb.velocity.y + dir.y, maxSpeed), 0);
    }

    void Rotate(Vector3 pos) // Rotate the player towards the given position
    {
        if ((player.transform.position - pos).magnitude > 0.001f)
        {
            Vector3 dif = -(player.transform.position - pos);
            float zRot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0, 0, zRot);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < hearts.transform.childCount; i++)
        {
            if (health / 2 < i + 1)
            {
                if (Mathf.Ceil(health / 2) < i + 1)
                {
                    hearts.transform.GetChild(i).GetComponent<Image>().sprite = null;
                    hearts.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
                else
                {
                    hearts.transform.GetChild(i).GetComponent<Image>().sprite = halfHeart;
                    hearts.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
                }
            }
            else
            {
                hearts.transform.GetChild(i).GetComponent<Image>().sprite = fullHeart;
                hearts.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            }
        }
    }
}
