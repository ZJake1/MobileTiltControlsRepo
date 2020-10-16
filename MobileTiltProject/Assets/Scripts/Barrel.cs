using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private GameObject barrel;
    private Rigidbody2D rb;
    private CapsuleCollider2D bc;

    private void Start()
    {
        barrel = gameObject;
        rb = barrel.GetComponent<Rigidbody2D>();
        bc = barrel.GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().health -= 1;
            Destroy(barrel);
        }
    }
}
