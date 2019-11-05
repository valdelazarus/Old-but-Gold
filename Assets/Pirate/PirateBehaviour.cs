using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBehaviour : MonoBehaviour
{
    private enum States { Patrol, Attack, Dead };
    States currentState = States.Patrol;
    Transform player;
    private int punchType;
    public int hits;
    private float AttackDistance = 3;
    private float patrolSpeed = 0.5f;
    private float attackSpeed = 0.5f;
    private float d2P;
    private bool isPlayerHidden = false;
    private bool canPunch = true;
    private bool isQuitting = false;
    public bool isDead;
    public bool isPunching;
    public bool isHit;
    public GameObject particles;
    public Animator anim;

    private float distanceToTarget;
    public Transform[] waypoints;
    private int currentWaypoint;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        hits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
        d2P = Vector3.Distance(transform.position, player.position);

        updateAnim();
        if (isDead)
        {
            ChangeState(States.Dead);
        }
        if (isHit || isPunching)
        {
            Invoke("ExitState", 0.2f);
        }
    }

    void FSM()
    {
        switch (currentState)
        {
            case States.Patrol:
                Patrol();
                break;
            case States.Attack:
                Attack();
                break;
            case States.Dead:
                Dead();
                break;
        }
    }

    private void ChangeState(States toState)
    {
        currentState = toState;
    }

    //States
    void Patrol()
    {
//follow waypoints
distanceToTarget= Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if (distanceToTarget <= 0.25f) currentWaypoint++;//next target
        if(currentWaypoint>waypoints.Length-1) currentWaypoint = 0;//restart


        Vector3 dir2W = waypoints[currentWaypoint].position - transform.position;
        float dS = 2 * Time.deltaTime;
        Vector3 newPos = transform.position + dir2W.normalized * dS;
        transform.position = newPos;
        //lookat waypoint without rotating
        transform.LookAt(waypoints[currentWaypoint]);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if (d2P <= AttackDistance)
        {
            ChangeState(States.Attack);
        }
        anim.SetFloat("Speed_f", Mathf.Abs(patrolSpeed));
    }

    void Dead()
    {

        anim.SetBool("isDead", true);
        Invoke("Die", 3);

    }


    void Attack()
    {
        
            if (d2P>AttackDistance)
            {
                ChangeState(States.Patrol);
            }

        //punch
        if (canPunch)
        {
            if (punchType == 0)
            {
                punchType = 1;
            }
            else
            {
                punchType = 0;
            }
            isPunching = true;
            anim.SetInteger("Punch_i", punchType);
            anim.SetBool("Punch_b", isPunching);
            canPunch = false;
            Invoke("CheckPunch", 2);//can only punch once every 2 seconds
        }

        //follow waypoints
        distanceToTarget = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if (distanceToTarget <= 0.25f) currentWaypoint++;//next target
        if (currentWaypoint > waypoints.Length - 1) currentWaypoint = 0;//restart


        Vector3 dir2W = waypoints[currentWaypoint].position - transform.position;
        float dS = 2 * Time.deltaTime;
        Vector3 newPos = transform.position + dir2W.normalized * dS;
        transform.position = newPos;



        //lookat player without rotating
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

    }



    private void CheckPunch()
    {

        canPunch = true;
    }

  

    public void IncrementHits(int hitCount)
    {
        isHit = true;
        hits += hitCount;
        if (hits >= 2) isDead = true;


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HUDManager.RemoveHealth(0.1f);
            //decrease player health
        }
    }



    void Die()
    {
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);

    }


    void updateAnim()
    {
        anim.SetBool("Punch_b", isPunching);
        anim.SetBool("hit", isHit);

    }

    private void ExitState()
    {
        isPunching = false;
        isHit = false;
    }

}
