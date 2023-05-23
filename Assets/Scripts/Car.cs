using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float speed;

    // Road road; //calling speed from road script >> upadte >> it worked but "NullReferenceException" error keep poping up. 

    public Transform spawnDirection;

    void Start()
    {
        // Vector3 carDirection = transform.forward;
        // Debug.Log(spawnDirection);
        
        
    }

   
    void Update()
    {
        if (spawnDirection.position.x < 0)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        if (spawnDirection.position.x > 0)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        
    }
}
