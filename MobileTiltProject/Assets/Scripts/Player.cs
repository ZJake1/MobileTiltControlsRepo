using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    private bool onMobile = false;

    public float health;
    public int maxHealth = 10;

    private float timeAlive = 0;

    public GameObject heartsUI;
    public GameObject timerText;
    public GameObject gameOverText;

    public GameObject pauseUI;
    
    public Sprite fullHeart;
    public Sprite halfHeart;

    public GameObject enemies;
    public GameObject projectiles;
    public GameObject barrels;

    private float baseMoveSpeed = 50;
    private float baseMaxSpeed = 2500;

    private float moveSpeed;
    private float maxSpeed;

    private Vector3 dir = new Vector3(0, 0, 0);

    private float respawnTimer = 3;
    
    // Called right before update
    private void Start() // Declare variables
    {
        player = gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        health = maxHealth;
        moveSpeed = baseMoveSpeed;
        maxSpeed = baseMaxSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        Respawn(); // Respawns the player if they are dead
        ChangeDirection(); // Change the movement direction for the move function
        Move(dir); // Move the character
        Rotate((player.transform.position + new Vector3(rb.velocity.x, rb.velocity.y, 0))); // Rotate the character
        UpdateUI(); // Updates all UI
    }

    void Respawn() // Checks if the player's health is less than or equal to 0 and respawns them if that is the case
    {
        if (health <= 0)
        {
            respawnTimer -= Time.deltaTime;
            moveSpeed = 0;
            gameOverText.SetActive(true);
            if (respawnTimer <= 0)
            {
                ClearArena();
                player.transform.position = new Vector3(0, 0, 0);
                health = maxHealth;
                respawnTimer = 3;
                moveSpeed = baseMoveSpeed;
                gameOverText.SetActive(false);
                timeAlive = 0;
            }
        }
    }

    void ClearArena() // Clears all enemies, projectiles and barrels in the arena;
    {
        ClearChildren(enemies);
        ClearChildren(projectiles);
        ClearChildren(barrels);
    }

    void ClearChildren(GameObject g) // Clears all children of the given GameObject
    {
        foreach (Transform c in g.transform) // Loops through each child in the game objects transform and destroys them
        {
            Destroy(c.gameObject);
        }
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
            dir = new Vector3(GetDirection().x, GetDirection().y, 0); // Set dir's x and y value to the Input.acceleration of the phone
        }
    }

    Vector3 GetDirection() // Find and return the direction of the tilted phone
    {
        Vector3 newDir = Input.acceleration * moveSpeed * Time.deltaTime;
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

    void UpdateUI() // Updates all UI to show the current variable values
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pauseUI.SetActive(true);
            }
            
        }
        if (health > 0)
        {
            timeAlive += Time.deltaTime;
        }
        timerText.GetComponent<Text>().text = Mathf.Floor(timeAlive).ToString();
        for (int i = 0; i < heartsUI.transform.childCount; i++) // For loop to check which heart icons should be there and which shouldn't
        {
            if (health / 2 < i + 1) // Check if the player's health is less than the loop index
            {
                if (Mathf.Ceil(health / 2) < i + 1) // Check if the player doesn't have half a health point and set the heart icon to null if this is the case
                {
                    heartsUI.transform.GetChild(i).GetComponent<Image>().sprite = null;
                    heartsUI.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
                else // If the player's health is on half set the heart icon to a half heart
                {
                    heartsUI.transform.GetChild(i).GetComponent<Image>().sprite = halfHeart;
                    heartsUI.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
                }
            }
            else // If the player's health is more than or equal to the loop index make the icon display a full heart
            {
                heartsUI.transform.GetChild(i).GetComponent<Image>().sprite = fullHeart;
                heartsUI.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col) // Collision check to see when the player touches an enemy
    {
        if (col.gameObject.tag == "Enemy") // Check for an enemy tag on the touched object
        {
            Destroy(col.gameObject); // Destroy the touched enemy
        }
    }
}
