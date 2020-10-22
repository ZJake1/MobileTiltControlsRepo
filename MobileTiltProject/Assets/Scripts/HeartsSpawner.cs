using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsSpawner : MonoBehaviour
{
    public GameObject heart;

    private GameObject player;

    private float timer = 10;

    private int screenWidth = 18;
    private int screenHeight = 10;

    // Start is called right as the script is instantiated
    private void Start()
    {
        player = GameObject.FindWithTag("Player"); // Finds the player in the scene through its tag
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(8.0f, 16.0f);
            int hor;
            int ver;
            if (Random.Range(0, 2) == 1)
            {
                if (Random.Range(0, 2) == 1)
                {
                    hor = screenWidth;
                }
                else
                {
                    hor = -screenWidth;
                }
                ver = Random.Range(-screenHeight, screenHeight + 1);
            }
            else
            {
                if (Random.Range(0, 2) == 1)
                {
                    ver = screenHeight;
                }
                else
                {
                    ver = -screenHeight;
                }
                hor = Random.Range(-screenWidth, screenWidth + 1);
            }
            GameObject currentHeart = Instantiate(heart, new Vector3(hor, ver, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)), transform); // Instantiates a heart outside the camera's borders
            Rigidbody2D curRb = currentHeart.GetComponent<Rigidbody2D>(); // Gets the RigidBody2D of the heart
            Vector3 dir = player.transform.position - currentHeart.transform.position; // Gets the direction vector towards the player
            curRb.velocity = dir + new Vector3(0, 0, Random.Range(-100, 100)); // Sets the velocity of the heart to move towards the player
        }
    }
}