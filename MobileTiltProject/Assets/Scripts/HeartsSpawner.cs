using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsSpawner : MonoBehaviour
{
    public GameObject heart;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.DeltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(5.0f, 10.0f);
            Instantiate(heart, this.transform);
        }
    }
}
