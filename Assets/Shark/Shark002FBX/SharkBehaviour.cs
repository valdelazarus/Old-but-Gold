using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour
{
    private enum States { Swimming, Jumping };
    States currentState = States.Swimming;

    private Transform player;
    private Animator anim;

    public float speed;
    private Vector3 dir;

    public float maxPlayerRange;
    private Vector3 StartPosition;
    private bool isJumping;
    private float maxJumpDistance;

    private float lastJump;
    private float jumpingTime;

    public float maxSwimmingRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        StartPosition = transform.position;
        isJumping = false;
        lastJump = 5;
        jumpingTime = 5;

        dir = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        //Player is in jumping range
        if(lastJump >= jumpingTime && player.transform.position.x >= this.transform.position.x - maxPlayerRange && player.transform.position.x <= this.transform.position.x + maxPlayerRange)
        {
            Debug.Log("Jump");
            maxJumpDistance = player.transform.position.y + 1;
            isJumping = true;
            lastJump = 0;
            transform.eulerAngles = new Vector3(-90, 180, 0);
            anim.SetTrigger("Bite");
            ChangeState(States.Jumping);
        }

        FSM();
        lastJump += Time.deltaTime;
    }

    void FSM()
    {
        switch (currentState)
        {
            case States.Swimming:
                Swim();
                break;
            case States.Jumping:
                Jump();
                break;
        }
    }

    private void ChangeState(States toState)
    {
        currentState = toState;
    }

    private void Swim()
    {
        //Go right
        if(transform.position.x <= StartPosition.x - maxSwimmingRange)
        {
            dir = Vector3.right;
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

        //Go left
        if (transform.position.x >= StartPosition.x + maxSwimmingRange)
        {
            dir = Vector3.left;
            transform.eulerAngles = new Vector3(0, -90, 0);
        }

        float dS = speed * Time.deltaTime;
        Vector3 newPos = transform.position + dir * dS;
        transform.position = newPos;
    }

    private void Jump()
    {
        if(transform.position.y >= maxJumpDistance)
        {
            isJumping = false;
        }

        if (isJumping)
        {
            float dS = speed * 2 * Time.deltaTime;
            Vector3 newPos = transform.position + Vector3.up * dS;
            transform.position = newPos;
        }
        else
        {
            float dS = speed * 2 * Time.deltaTime;
            Vector3 newPos = transform.position + Vector3.down * dS;
            transform.position = newPos;
        }

        if(transform.position.y <= StartPosition.y && !isJumping)
        {
            if(dir == Vector3.left)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }

            GetComponent<BoxCollider>().enabled = true;
            anim.SetTrigger("Swim");
            ChangeState(States.Swimming);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            HUDManager.RemoveHealth(0.1f);
        }
    }
}
