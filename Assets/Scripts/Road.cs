using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject carPre; 
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    public float spawnInterval = 2f;

    private Transform spawnPoint;

    //variables for car script    // we are also getting and sending transform spawn point that is left or right to car script. car will choose to move accordingly.
    public float carspeed; //so that car speed doesnt get updated & changed regulary in car spawn script. 
    
    

    void Start()
    {
        //speed to send in car script
        carspeed = Random.Range(1, 7);
        //----- 

        spawnPoint = Random.Range(0, 2) == 0 ? leftSpawnPoint : rightSpawnPoint;

        InvokeRepeating("SpawnCar", 0f, spawnInterval);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnCar()
    {
       
        // Instantiate the car object at the chosen spawn point
        GameObject newCar = Instantiate(carPre, spawnPoint.position, spawnPoint.rotation);         
        newCar.GetComponent<Car>().speed = carspeed; // modifying the valye of carspeed into speed variable in car script that is attached to car
        newCar.GetComponent<Car>().spawnDirection = spawnPoint; //will send spawnpoint tranfrom of this script into spawn direction transform of car script attahed to car
        
        //
        
    }

}





