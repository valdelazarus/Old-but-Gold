using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    private const float initialYRotation = 180f;
    private const float rotateAmount = 90f;
    public float speed;
    public float jumpForce;
    public float gravityModifier;

    Rigidbody rb;
     Animator anim;
    float actualSpeed;
    float movement;

    public static bool isPunching;
    public static bool isThrowing;
    bool isJumping;
    bool isOnGround;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        Physics.gravity *= gravityModifier;
    }

    
    void Update()
    {
        ProcessHorizontalMovement();
        RotateTowardsWalkingDirection();
        ProcessJumping();
        ProcessPunching();
        ProcessThrowing();
        UpdateAnim();
    }

    private void ProcessHorizontalMovement()
    {
        movement = CrossPlatformInputManager.GetAxis("Horizontal");
        actualSpeed = movement * speed;
        rb.velocity = Vector3.right * actualSpeed;
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


        }



    }

    void ProcessJumping()
    {
        CheckIfOnGround();
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isOnGround && !isJumping)
        {
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            isJumping = true;
            isOnGround = false;
        }
        
  
    }

    

    void CheckIfOnGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        float maxD = .1f;

        if (Physics.Raycast(ray, out hit, maxD))
        {
            if (hit.collider != null)
            {
                isOnGround = true;
                isJumping = false;
            }
            else {
                isOnGround = false;
            }
        }
    }

    void UpdateAnim()
    {
        anim.SetFloat("Speed_f", Mathf.Abs(actualSpeed));
        anim.SetBool("Jump_b", isJumping);
        anim.SetBool("Punch_b", isPunching);
        anim.SetBool("Throw_b", isThrowing);
    }

    void InstantiateRock(GameObject rock)
    {
        Debug.Log("Rock");
        //Instantiate(rock);
    }
}
