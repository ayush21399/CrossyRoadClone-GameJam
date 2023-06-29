using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogo : MonoBehaviour
{

    //NOTE : REWRITE WHOLE WALKING-JUMPING SCRIPT BCZ OF COLLISION //update : It Works Now, but need to smoothen it in future.

    public float horizontalInput;
    public float verticalInput;

    public float gridsize = 1f;
    public float movespeed = 7f;

    public float stutter = 0.2f;

    public float jumphight = 0.15f;
    public float jumptime = 0.15f;

    Rigidbody rb; //have'nt used rb yet

    private Vector3 targetpos;
    private bool isMoving = false;

    //private bool movecheck = true; private Vector3 oldpos;

    Vector3 startPosition;
    Vector3 jumpTargetPosition;

    //to limit players backward movement. edit maxbackdist to controll the backward movement limit, highestzpos will update with highest z postion of player.
    public float maxBackDist = 5f;
    public float highestZpos = 0;

    //
    public float XaxisBound = 7f;
    public float riverXaxisBound = 7.3f;
    //

    //
    public float ResturnPoint = 70f;
    //

    //to help in ui
    public float levelUI = 0f;
    public bool GameOOver = false;

    //child direction to detact ray, 4 direction checkers. ----- not using as we directly shoot ray from dog
    ///public Transform forwardcheck;
    ///public Transform backwardcheck;
    ///public Transform leftcheck;
    ///public Transform rightcheck;
    //------
    public float raycastdist = 1f; // make it 1
    public float raycastinterval = 2f;
    public float timer = 0f;
    private Ray rayfor; private RaycastHit hitfor; public bool forr = false;
    private Ray raybac; private RaycastHit hitbac; public bool bacc = false;
    private Ray raylef; private RaycastHit hitlef; public bool leff = false;
    private Ray rayrig; private RaycastHit hitrig; public bool rigg = false;




   // public bool HorInputController = false;
   // public bool VerInputController = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //have'nt used rigidbody yet

        targetpos = transform.position;

        // ----------- 4 direction checkers-------- not using right now, we directly cast ray from dog
        //forwardcheck = transform.Find("ForwardCheck");
        //backwardcheck = transform.Find("BackwardCheck");
        //leftcheck = transform.Find("LeftCheck");
        //rightcheck = transform.Find("RightCheck");
        //

        //Debug.Log(forwardcheck.transform.position) ; Debug.Log(backwardcheck.transform.position);  Debug.Log(leftcheck.transform.position);  Debug.Log(rightcheck.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //re sending player to the base ground so that game world doesnt continue to expand, also restating default value for targetpos and highestZpos
        if (transform.position.z >= ResturnPoint)
        {
            Vector3 newposition = new Vector3(transform.position.x, transform.position.y, 2);
            transform.position = newposition;
            targetpos = new Vector3(transform.position.x,transform.position.y,0);
            highestZpos = 0;

            levelUI++; //helping in UI script.
        }
            //remove all the cluter ground object which didnt get deleted by raycast of spawner.
        if (transform.position.z >= (ResturnPoint - 2))
        {
            string[] objRemove = { "Road", "Grass", "Water" };

            foreach (string tag in objRemove)
            {
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject gameObject in gameObjects)
                {
                    Destroy(gameObject);
                }
            }
        }
        //-------------------
        //-----collision detection-----

        //-----------------------------

        ///4 direction spawner ray check
        timer++;
        if (timer % raycastinterval == 0)
        {
            raycheck();
        }
        //

        ismovingcheck();

        gameover(); //gameover
    }

    public void raycheck()
    {
        rayfor = new Ray(transform.position, Vector3.forward);
        //RaycastHit hitfor;

        raybac = new Ray(transform.position, Vector3.back);
        //RaycastHit hitbac;

        raylef = new Ray(transform.position, Vector3.left);
        //RaycastHit hitlef;

        rayrig = new Ray(transform.position, Vector3.right);
        //RaycastHit hitrig;

        //setting up boolean depending on what path (for bac lef rig) is getting blokced. this vallues gets passed to ismovingcheck() 
        if (Physics.Raycast(rayfor, out hitfor, raycastdist) && hitfor.collider.CompareTag("Tree"))
        {
            forr = true;
        }
        else { forr = false; }
        if (Physics.Raycast(raybac, out hitbac, raycastdist) && hitbac.collider.CompareTag("Tree"))
        {
            bacc = true;      
        }
        else { bacc = false; }
        if (Physics.Raycast(raylef, out hitlef, raycastdist) && hitlef.collider.CompareTag("Tree"))
        {
            leff = true;
        }
        else { leff = false; }
        if (Physics.Raycast(rayrig, out hitrig, raycastdist) && hitrig.collider.CompareTag("Tree"))
        {
            rigg = true;
        }
        else { rigg = false; }

        //Debug.DrawRay(rayfor.origin, rayfor.direction * raycastdist, Color.red);
        // else


        //  if (Physics.Raycast(raytwo, out hittwo, raycastdist))
        // {
        //     despawning(hittwo);
        // }
    }


    public void ismovingcheck()
    {
        if (!isMoving)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            //----------- checking blockage in front back or left right (ray cast will controll bools), if path is blocked ver-hor input will get zero depending on what path gets blocked. 
            if (forr == true && verticalInput == 1)
            {
                verticalInput = 0;
            }
            if (bacc == true && verticalInput == -1)
            {
                verticalInput = 0;
            }
            if (leff == true && horizontalInput == -1)
            {
                horizontalInput = 0;
            }
            if (rigg == true && horizontalInput == 1)
            {
                horizontalInput = 0;
            }
            //------------

            //  Checking diagonal movement and canceling it -- checking if the horizontal value and vertical value are more/less than 0, both values will be set to 0 canceling diagonal movement.
            if ((horizontalInput > 0.1 || horizontalInput < -0.1f) && (verticalInput > 0.1 || verticalInput < -0.1f))
            {
                horizontalInput = 0; verticalInput = 0;
            }
            //------
            // to keep player within bound of limited x axis,
            if ((transform.position.x <= -XaxisBound && horizontalInput == -1) || (transform.position.x >= XaxisBound && horizontalInput == 1))
            {
                horizontalInput = 0;
            }
            //------
            // player will only be able to move back untill certain distance, dist will get updated as player keps moving forward.           
            if (transform.position.z > highestZpos)
            {
                highestZpos = transform.position.z;
                
            }            
            if(verticalInput < -0.1 && highestZpos - maxBackDist > transform.position.z)
            {
                    verticalInput = 0;
            }
            //---------

            if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            {

                // Vector3Int targetGridPosition = new Vector3Int(Mathf.RoundToInt(targetpos.x / gridsize), Mathf.RoundToInt(targetpos.y / gridsize), Mathf.RoundToInt(targetpos.z / gridsize));
                // commented reason: it was keeping y position to 0 or 1 , and not in between at 0.6. it was rounding it up.
                Vector3 targetGridPosition = new Vector3(Mathf.Round(targetpos.x / gridsize), targetpos.y, Mathf.Round(targetpos.z / gridsize));
                targetGridPosition += new Vector3Int(Mathf.RoundToInt(horizontalInput), 0, Mathf.RoundToInt(verticalInput));
                targetpos = new Vector3(targetGridPosition.x * gridsize, targetGridPosition.y * gridsize, targetGridPosition.z * gridsize);

                StartCoroutine(movtotar());
            }

        }
    }

    //----------movement & jumping -------------------
    private IEnumerator movtotar()
    {

        isMoving = true;

        float distance = Vector3.Distance(transform.position, targetpos);

        yield return JumpAnimation();

        while (distance > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetpos, movespeed * Time.deltaTime);

            distance = Vector3.Distance(transform.position, targetpos);

            yield return null;
        }

        transform.position = targetpos;

        // rb.AddForce(0, 2f, 0); isnt smooth enough

        yield return new WaitForSeconds(stutter);

        isMoving = false;


    }


    private IEnumerator JumpAnimation()
    {
        float elapsedTime = 0f;
        startPosition = transform.position;
        jumpTargetPosition = targetpos + Vector3.up * jumphight;

        while (elapsedTime < jumptime)
        {
            float t = elapsedTime / jumptime;
            transform.position = Vector3.Lerp(startPosition, jumpTargetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Snap to the jump target
        transform.position = jumpTargetPosition;

    }
    //-----------------------------
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("LOG"))
        {
            //Debug.Log("log che bhai");
            GameObject gameobj = collision.gameObject;
            Vector3 movetransform = new Vector3(gameobj.transform.position.x, this.transform.position.y, this.transform.position.z);
            transform.position = movetransform;

            targetpos.x = movetransform.x; //edit this field to change new transfer position. solved error with this line when player snap to x position where he boarded log.  
        }                                  //play with this line if snaping issue occurs, nothing else was changed for that error.

    }

    private void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.CompareTag("Tree"))
        // {
        //transform.position = targetpos;
        //Destroy(gameObject);
        //rb.AddForce(startPosition);
        //isMoving = true;
        //movecheck = false;

        // }
        //checking water collision will make game over
        if (collision.gameObject.CompareTag("Water") || collision.gameObject.CompareTag("Car"))
        {
            gameoverscreen();
            //Destroy(gameObject);
        }
    }



    /*
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("LOG"))
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
    */
    //-----------Collision detection-----
    public void gameover()
    {
        if (transform.position.x> riverXaxisBound || transform.position.x < -riverXaxisBound)
        {
            //Destroy(gameObject); //game over screen
            gameoverscreen();
        }

    }
    public void gameoverscreen()
    {
        Time.timeScale = 0f;
        GameOOver = true;
    }

}