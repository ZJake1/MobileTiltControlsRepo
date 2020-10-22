using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Player pl = col.GetComponent<Player>();
            if (pl.health < pl.maxHealth)
            {
                Destroy(gameObject);
                if (pl.health + 1 >= pl.maxHealth)
                {
                    pl.health += 1;
                }
                else
                {
                    pl.health += 2;
                }
            }
        }
    }
}
