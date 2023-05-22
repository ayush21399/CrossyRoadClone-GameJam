using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogo : MonoBehaviour
{
    public float gridsize = 1f;
    public float movespeed = 10f;

    public float stutter = 0.2f;

    public float jumphight = 0.25f;
    public float jumptime = 0.15f;

    Rigidbody rb;

    private Vector3 targetpos;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent <Rigidbody>();

        targetpos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (!isMoving)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            {

                // Vector3Int targetGridPosition = new Vector3Int(Mathf.RoundToInt(targetpos.x / gridsize), Mathf.RoundToInt(targetpos.y / gridsize), Mathf.RoundToInt(targetpos.z / gridsize));
                // commented reason: it was keeping y position to 0 or 1 , and not in between at 0.6. it was rounding it up.
                Vector3 targetGridPosition = new Vector3(Mathf.Round(targetpos.x / gridsize),targetpos.y, Mathf.Round(targetpos.z / gridsize));
                targetGridPosition += new Vector3Int(Mathf.RoundToInt(horizontalInput), 0, Mathf.RoundToInt(verticalInput));
                targetpos = new Vector3(targetGridPosition.x * gridsize, targetGridPosition.y * gridsize, targetGridPosition.z * gridsize);

                StartCoroutine(movtotar());
            }

        }

    }


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
        Vector3 startPosition = transform.position;
        Vector3 jumpTargetPosition = targetpos + Vector3.up * jumphight;

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





}