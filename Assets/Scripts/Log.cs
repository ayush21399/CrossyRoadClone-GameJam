using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public float speed;
    public float log_length;
    // Road road; //calling speed from road script >> upadte >> it worked but "NullReferenceException" error keep poping up. 
    public Transform leftspawn;
    public Transform rightspawn;

    public Transform spawnDirection;

    void Start()
    {
        // Vector3 carDirection = transform.forward;
        // Debug.Log(spawnDirection);

        log_length = Random.Range(0.1f, 0.4f);
        transform.localScale = new Vector3(log_length, transform.localScale.y, transform.localScale.z);
    }


    void Update()
    {
        //if (spawnDirection == null || leftspawn == null || rightspawn == null)
        // {
        //     Destroy(gameObject);
        // }
        if (spawnDirection != null)
        {
            if (spawnDirection.position.x < 0)
            {
                transform.position += Vector3.right * Time.deltaTime * speed;

                if (transform.position.x > rightspawn.position.x)
                { Destroy(gameObject); }
            }
            if (spawnDirection.position.x > 0)
            {
                transform.position += Vector3.left * Time.deltaTime * speed;

                if (transform.position.x < leftspawn.position.x)
                { Destroy(gameObject); }
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
