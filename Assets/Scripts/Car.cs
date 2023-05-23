using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    

    public float speed;

    // Road road; //calling speed from road script >> upadte >> it worked but "NullReferenceException" error keep poping up. 



    void Start()
    {
        // Vector3 carDirection = transform.forward;
       
         
    }

   
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
    }
}
