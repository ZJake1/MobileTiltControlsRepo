using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    bool used = false;

    Vector3 startSize;
    float sizeMulti = 0f;

    void Start()
    {
        startSize = transform.localScale;
    }

    void Update()
    {
        if (used == true)
        {
            if (transform.localScale.magnitude > startSize.magnitude * 2)
                Destroy(gameObject);

            transform.localScale += startSize * sizeMulti;
            sizeMulti += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && used == false)
        {
            Player pl = col.GetComponent<Player>();
            if (pl.health < pl.maxHealth)
            {
                used = true;
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
