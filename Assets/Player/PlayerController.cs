﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    private const float initialYRotation = 180f;
    private const float rotateAmount = 90f;
    public float speed;
    public float jumpForce;
    //public float gravityModifier; REMOVED. NOT NEEDED
    
    public GameObject rockPrefab;
    public GameObject rockSpawn;
    public float rockSpeed = 15.0f;

    Rigidbody rb;
     Animator anim;
    float actualSpeed;
    float movement;

    public static bool isPunching;
    public static bool isThrowing;
    //public bool isJumping; REMOVED. NOT NEEDED
    bool isOnGround;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        //Physics.gravity *= gravityModifier; REMOVED. NOT NEEDED
    }

    
    void Update()
    {
        ProcessHorizontalMovement();
        RotateTowardsWalkingDirection();
        ProcessPunching();
        ProcessThrowing();
        UpdateAnim();
    }

    private void FixedUpdate()
    {
        ProcessJumping();
    }

    private void ProcessHorizontalMovement()
    {
        movement = CrossPlatformInputManager.GetAxis("Horizontal");
        actualSpeed = movement * speed;
        //rb.velocity = Vector3.right * actualSpeed; REMOVED/CHANGED SO THAT IT DOESN'T AFFECT THE JUMP VELOCITY, ONLY THE HORIZONTAL MOVEMENT
        rb.velocity = new Vector3(actualSpeed, rb.velocity.y, 0.0f);
    }

    void RotateTowardsWalkingDirection()
    {
        transform.rotation = Quaternion.Euler(0f, initialYRotation - rotateAmount * movement, 0f);
    }

    void ProcessPunching()
    {
       
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && !isPunching)
        {
            
            isPunching = true;
           

        }
        


    }

    void ProcessThrowing()
    {

        if (CrossPlatformInputManager.GetButtonDown("Fire2") && !isThrowing)
        {

            isThrowing = true;

            ThrowRock(); 
        }



    }

    void ProcessJumping()
    {
        CheckIfOnGround();
        if (Input.GetButtonDown("Jump") && isOnGround)// && !isJumping) REMOVED
        {
            //transform.Translate(Vector3.up * jumpForce * Time.deltaTime); REMOVED/CHANGED TO ADDFORCE
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isJumping = true; REMOVED
            isOnGround = false;
            anim.SetTrigger("jumped");
        }
        
  
    }

    

    void CheckIfOnGround()
    {
        Ray[] rays = new Ray[3];
        rays[0] = new Ray(transform.position - Vector3.right * .45f, Vector3.down);
        rays[1] = new Ray(transform.position, Vector3.down);
        rays[2] = new Ray(transform.position + Vector3.right * .45f, Vector3.down);

        RaycastHit hit;
        float maxD = .1f;

        foreach (Ray ray in rays)
        {
            if (Physics.Raycast(ray, out hit, maxD))
            {
                if (hit.collider != null)
                {
                    isOnGround = true;
                    //isJumping = false; REMOVED
                }
                else
                {
                    isOnGround = false; 
                }
            }
        }
    }

    void UpdateAnim()
    {
        anim.SetFloat("Speed_f", Mathf.Abs(actualSpeed));
        //anim.SetBool("Jump_b", isJumping); REMOVED
        anim.SetBool("Punch_b", isPunching);
        anim.SetBool("Throw_b", isThrowing);
    }

    void ThrowRock() // CHANGED: InstantiateRock to ThrowRock,  and  REMOVED: (GameObject rock)
    {
        Debug.Log("Rock");
        //Instantiate(rock);
        GameObject r = (GameObject)Instantiate(rockPrefab, transform.position, transform.rotation);
        Rigidbody rb = r.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * rockSpeed;
        Destroy(r, 3); // Destroy rock after n seconds
    }
}
