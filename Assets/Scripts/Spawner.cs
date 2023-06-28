using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    //NOTE need spawner's x & y position to be zero but Z position will move with dog.


    public GameObject road;
    public GameObject grass;
    public GameObject water;

    public GameObject Dogo;

    public Transform upSpawn;
    public Transform downSpawn;

    public Transform spawnexactpost; 

    public float raycastdist = 3f;
    public float raycastinterval = 10f;
    public float timer = 0f;

    void Start()
    {
        upSpawn = transform.Find("UpSpawn");
        downSpawn = transform.Find("DownSpawn");

        //StartCoroutine(raytime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3( 0,0,Dogo.transform.position.z); //to keep spawnmanager with dog's position. but not want to move him in Y-Z direction


        //-------raycast below
        //--------------------------- there are 3 ways to raycast within fixed amount of time
        //1> directly mention raycheck() in update but it will check in every frame update, using quite a bit of resources
        //2> startcoroutine with raytime funtion (commented bellow) was leaving gap between grid, and wasnt fast enough.
        //3> manual timer, raycastinterval is our frame limit, so after every 10th frame we will shoot raycast. timmer gets ++ with every frame, 
        //   when it reached 10 calculation occuers. 
        
        //raycheck();

        timer++;
        if (timer % raycastinterval == 0)
        {
            raycheck();
        }

        //---------

    }


    //------------------------playing with ray---------------------\\

    /* ----used in coruntine funtion
    public IEnumerator raytime()
    {
        while(true)
        {
            raycheck();

            yield return new WaitForSecondsRealtime(raycastinterval);
        }
    }
    */
    public void raycheck()
    {
        Ray ray = new Ray(upSpawn.position, Vector3.down);
        RaycastHit hit;

        Ray raytwo = new Ray(downSpawn.position, Vector3.down);
        RaycastHit hittwo;

        if (Physics.Raycast(ray, out hit, raycastdist))
        {
            //Debug.Log(hit.point.y);
            return;
        }
        else
        spawning();

        if (Physics.Raycast(raytwo, out hittwo, raycastdist))
        {
            despawning(hittwo);
        }
    }
    public void spawning()
    {
        GameObject spawnobj;
        int spawnidx = Random.Range(0, 3);

        switch (spawnidx)
        {
            case 0:
                spawnobj = road;
                break;
            case 1:
                spawnobj = grass;
                break;
            case 2:
                spawnobj = water;
                break;
            default:
                spawnobj = grass;
                break;
        }

        Vector3 spawnexactpost = new Vector3(0, 0, Mathf.RoundToInt(upSpawn.position.z)); //we can use upspawn, but upspawn position is different, we need to spawn road in x-y 0-0 and changed z.
        Instantiate(spawnobj, spawnexactpost, Quaternion.identity);

    }
    public void despawning(RaycastHit hittwo)
    {
        Destroy(hittwo.collider.gameObject);
    }
    //------------------------playing with ray---------------------\\

}
