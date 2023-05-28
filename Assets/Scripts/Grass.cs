using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public GameObject treePrefab;
    public int minTrees = 10;
    public int maxTrees = 20;
    public int minX = -15;
    public int maxX = 15;

    void Start()
    {
        int numTrees = Random.Range(minTrees, maxTrees);

        for (int i = 0; i < numTrees; i++)
        {
            float treespawnarea = Random.Range(minX, maxX);
            //treespawnarea = treespawnarea + 0.5f; //to remmove error where player's landing position can be in tree's position, due to that we had issues with collider.
                                                  //Debug.Log(treespawnarea);
            Instantiate(treePrefab, new Vector3(treespawnarea, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
