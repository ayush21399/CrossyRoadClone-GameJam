using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    //NOTE need spawner's x & y position to be zero but Z position will move with dog.


    public GameObject road;
    public GameObject grass;
    public GameObject water;

    public GameObject ground;

    public GameObject Dogo;

    public Transform upSpawn;
    public Transform downSpawn;

    public Transform spawnexactpost;

    // using script between certain points to avoid unesscory spawning when player is returning to base.
    public float maxDistAllowSpawning = 68f;
    public float minDistAllowSpawning = 2f;
    public float ContniueSpawningGround = 50f;
    //

    public float raycastdist = 3f;
    public float raycastinterval = 5f;
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
            if (Dogo.transform.position.z < maxDistAllowSpawning && Dogo.transform.position.z > minDistAllowSpawning) //to keep spawning active only between certain points,
            {                                                                                                        // - contnue: or else it will keep spawning when we make player return back to base.
                raycheck(); //Debug.Log("active");
            }//else { Debug.Log("un"); }
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

        Ray raytwo = new Ray(downSpawn.position, Vector3.down); //shoting ray up from beneth the gruond to avoid floating obstacles > Update2: cant detect plane when we shoot ray up from below ground. 
        //RaycastHit hittwo; 
        RaycastHit[] hittwo = Physics.RaycastAll(raytwo, raycastdist); // functional despawining, use if error occurs

        //Debug.DrawRay(raytwo.origin, raytwo.direction * raycastdist, Color.red);
        
        if (Physics.Raycast(ray, out hit, raycastdist))
        {
            //Debug.Log(hit.point.y);
            //return; //Uncommenting this return statement will exit the entire function when ground is detected, preventing despawning from executing.
                      //during round 2, there will be "ground (that cant be deleted)" from round 1, so when player's upspawn reaches the ground it will start "return"
                      //and last few active lane wont get deleted >> tho those active lane can be deleted from the DOGO's update scriptchecking and clearing the field for the last object.
        }
        else
        spawning();

        //------------ functional despawining, use if error occurs //commenting just to use ray-shoot-up method // reason to avoid foreach loop // Update2> cant detact plane whn we shot ray frm beneath
        foreach (RaycastHit hits in hittwo)
        {
            despawning(hits);
            
        }
        //----- functional despawining, use if error occurs
        //if (Physics.Raycast(raytwo, out hittwo, raycastdist) && !hittwo.collider.CompareTag("mainPlain"))
        //{
        //   //Debug.Log(hittwo.collider.tag);
        //    despawning(hittwo);
        //}
        //-------
        //-------------
    }
    public void spawning()
    {
        GameObject spawnobj;
        int spawnidx = Random.Range(0, 3);

        switch (spawnidx)
        {
            case 0:
                spawnobj = road;
                //spawnobj = ground;
                break;
            case 1:
                spawnobj = grass;
                //spawnobj = ground;
                break;
            case 2:
                spawnobj = water; 
                //spawnobj = ground;
                break;
            default:
                spawnobj = grass;
                //spawnobj = ground;
                break;
        }

        //spawing ground after cxertain distance, build up for player position reset to base
        if (Dogo.transform.position.z >= ContniueSpawningGround)
        {
            spawnobj = ground;
            //if (Dogo.transform.position.z >= 30)
            //{
            //    Vector3 newposition = new Vector3(Dogo.transform.position.x, Dogo.transform.position.y, 2);
            //    Dogo.transform.position = newposition;
            // }

            //------- to remove all the leftover before starting from the base again.
                    // left over removal code now in DOGO.Update (in if statement)
            //--------
        }
        //--------

        Vector3 spawnexactpost = new Vector3(0, 0, Mathf.RoundToInt(upSpawn.position.z)); //we can use upspawn, but upspawn position is different, we need to spawn road in x-y 0-0 and changed z.
        Instantiate(spawnobj, spawnexactpost, Quaternion.identity);

    }
    public void despawning(RaycastHit hittwo)
    {
        if (hittwo.collider != null)
        {            
            //----- when ray hit log it was only removeing log not water, now water will also get removed with log.
            if (hittwo.collider.CompareTag("Road") || hittwo.collider.CompareTag("Water") || hittwo.collider.CompareTag("Grass"))
            {
                Destroy(hittwo.collider.gameObject); //Destroy(hittwo.collider.gameObject.transform.parent.gameObject);
            }
            //-----
            
            //Debug.Log(hittwo.collider.tag);
            //Destroy(hittwo.collider.gameObject);
        }
    }
    //------------------------playing with ray---------------------\\

}
