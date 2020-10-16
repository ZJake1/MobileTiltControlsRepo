using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    private int screenWidth = 18;
    private int screenHeight = 10;

    private float waitTimer = 5f;

    // Update is called once per frame
    void Update()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            waitTimer = Random.Range(10, 20);
            for (int i = 0; i < Random.Range(2, 4); i++)
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
                Instantiate(enemy, new Vector3(hor, ver, 0), Quaternion.identity, transform);
            }
        }
    }
}
