using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public GameObject barrel;

    private GameObject player;

    private int screenWidth = 18;
    private int screenHeight = 10;

    private float waitTimer = 10;

    // Start is called right as the script is instantiated
    private void Start()
    {
        player = GameObject.FindWithTag("Player"); // Finds the player in the scene through its tag
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer -= Time.deltaTime; // Negates time from the waitTimer
        if (waitTimer <= 0) // Checks if the waitTimer is less than or equal to 0
        {
            waitTimer = Random.Range(5.0f, 15.0f); // Sets a new random waitTimer between the given values
            for (int i = 0; i < Random.Range(1, 3); i++) // Loops a random number of times between the given values
            {
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
                GameObject currentBarrel = Instantiate(barrel, new Vector3(hor, ver, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)), gameObject.transform); // Instantiates a barrel outside the camera's borders
                Rigidbody2D curRb = currentBarrel.GetComponent<Rigidbody2D>(); // Gets the RigidBody2D of the barrel
                Vector3 dir = player.transform.position - currentBarrel.transform.position; // Gets the direction vector towards the player
                curRb.velocity = dir + new Vector3(0, 0, Random.Range(-100, 100)); // Sets the velocity of the barrel to move towards the player
            }
        }
    }
}
