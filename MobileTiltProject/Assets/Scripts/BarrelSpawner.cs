using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public GameObject barrel;

    private GameObject player;

    private int screenWidth = 18;
    private int screenHeight = 10;

    private float waitTimer = 15;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            waitTimer = Random.Range(5, 15);
            for (int i = 0; i < Random.Range(1, 3); i++)
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
                GameObject currentBarrel = Instantiate(barrel, new Vector3(hor, ver, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)), gameObject.transform);
                Rigidbody2D curRb = currentBarrel.GetComponent<Rigidbody2D>();
                Vector3 dir = player.transform.position - currentBarrel.transform.position;
                curRb.velocity = dir + new Vector3(0, 0, Random.Range(-100, 100));
            }
        }
    }
}
