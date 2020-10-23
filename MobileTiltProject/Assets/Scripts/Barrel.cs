using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private GameObject barrel;
    private Rigidbody2D rb;
    private CapsuleCollider2D bc;

    bool used = false;

    Vector3 startSize;
    float sizeMulti = 0.0f;

    private void Start()
    {
        barrel = gameObject;
        rb = barrel.GetComponent<Rigidbody2D>();
        bc = barrel.GetComponent<CapsuleCollider2D>();

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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player" && used == false)
        {
            used = true;
            col.gameObject.GetComponent<Player>().health -= 1;
        }
    }
}
