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

    void Start()
    {
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
    }

}





