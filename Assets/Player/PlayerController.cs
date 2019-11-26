using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    //private const float initialYRotation = 180f;
    //private const float rotateAmount = 90f;
    public float speed;
    public float jumpForce;
    //public float gravityModifier; REMOVED. NOT NEEDED
    
    public GameObject rockPrefab;
    public GameObject rockSpawn;
    public float rockSpeed = 15.0f;
    public GameObject rightHand;

    public GameObject punchHitBox;

   // public GameObject rockPrefab;
   // public GameObject rockSpawn;
   // public float rockSpeed = 15.0f;

    Rigidbody rb;
    public static Animator anim;
    float actualSpeed;
    float movement;

    public bool isPunching;
    public bool isThrowing;
    public bool canJump;
    bool isOnGround;

    public int punchStrength;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 1);//activate throwing layer to keep running
        canJump = true;
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

    private void InteractWithNPC()
    {
        Vector3 vec = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Debug.DrawRay(vec, transform.forward, Color.green);
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(vec, transform.forward, out hitInfo, 1.5f))
            {
                // update the tag depending on npc
                if (hitInfo.collider.tag == "NPCTest")
                {
                    string[] sentences = { "Blah", "More diag" }; // should be set in NPC script
                    GameObject.Find("DialogManager").GetComponent<DialogManager>().ShowDialog(sentences);
                }
            }
        }
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
        //transform.rotation = Quaternion.Euler(0f, initialYRotation - rotateAmount * movement, 0f);
        float lastMovement = 0;
        
        if (movement > 0)
        {
            lastMovement = 1f;
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        } else if (movement < 0)
        {
            lastMovement = -1f;
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        } else
        {
            if (lastMovement > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }
            else if (lastMovement < 0)
            {
                transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            }
        }
    }

    void ProcessPunching()
    {

        if (CrossPlatformInputManager.GetButtonDown("Fire1") && !isPunching && GameObject.Find("DialoguePanel") == null)
        {

            isPunching = true;

            
        }



    }

    void ProcessThrowing()
    {

        if (CrossPlatformInputManager.GetButtonDown("Fire2") && !isThrowing)
        {

            isThrowing = true;
            //ThrowRock();//CHANGED: now called by animation event for perfect timing
        }



    }

    void ProcessJumping()
    {
        CheckIfOnGround();
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isOnGround && canJump)
        {
            canJump = false;
            //transform.Translate(Vector3.up * jumpForce * Time.deltaTime); REMOVED/CHANGED TO ADDFORCE
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            anim.SetTrigger("jumped");

            //play jump sound
            GetComponent<PlayerSFX>().PlayJump();

            Invoke("enableJump", 1f);
        }
    }

    void enableJump()
    {
        canJump = true;
    }

    void CheckIfOnGround()
    {
        Ray[] rays = new Ray[20];
        for (int i = 0; i < 20; i++)
        {
            rays[i] = new Ray(transform.position - Vector3.right + Vector3.right * i * .1f, Vector3.down);
        }

        RaycastHit hit;
        float maxD = .1f;

        foreach (Ray ray in rays)
        {
            if (Physics.Raycast(ray, out hit, maxD))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.blue);
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
        Debug.Log("Rock thrown!");
        //Instantiate(rock);
        GameObject r = (GameObject)Instantiate(rockPrefab, rightHand.transform.position, transform.rotation);//changed to originate from player hand
        Rigidbody rb = r.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * rockSpeed;
        Destroy(r, 5); // Destroy rock after n seconds
    }

    public void EnablePunchHitBox()
    {
        punchHitBox.SetActive(true);
        GetComponent<PlayerSFX>().PlayPunch();
    }
    public void DisablePunchHitBox()
    {
        punchHitBox.SetActive(false);
    }

    public void addSpeed(float amount, float speedPowerupTime)
    {
        speed += amount;
        HUDManager.DisplaySpeedPowerup();
        StartCoroutine(speedPowerupTimer(amount, speedPowerupTime));
    }

    private IEnumerator speedPowerupTimer(float amount, float speedPowerupTime)
    {
        yield return new WaitForSeconds(speedPowerupTime);
        speed -= amount;
        HUDManager.HideSpeedPowerup();
    }

    public void addStrength(int amount, float strengthPowerupTime)
    {
        punchStrength += amount;
        rockPrefab.GetComponent<RangedDetection>().rockStrength += amount;
        HUDManager.DisplayStrengthPowerup();
        StartCoroutine(strengthPowerupTimer(amount, strengthPowerupTime));
    }

    private IEnumerator strengthPowerupTimer(int amount, float strengthPowerupTime)
    {
        yield return new WaitForSeconds(strengthPowerupTime);
        punchStrength -= amount;
        rockPrefab.GetComponent<RangedDetection>().rockStrength -= amount;
        HUDManager.HideStrengthPowerup();
    }
}
