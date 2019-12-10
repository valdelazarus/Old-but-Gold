using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBehaviour : MonoBehaviour
{
    private enum States { Patrol, Attack, Dead };
    States currentState = States.Patrol;
    public GameObject parrot;
    Transform player;
    public int hits;
    private float AttackDistance = 4;
    private float patrolSpeed = 0.5f;
    private float d2P;
    public bool isDead;
    public bool isHit;
    public bool canThrow=true;
    public bool isThrowing = false;
    public GameObject particles;
    public GameObject rightHand;
    public Animator anim;

    public float throwParrotRate;

    private float distanceToTarget;
    public Transform[] waypoints;
    private int currentWaypoint;

    public AudioClip dyingSound;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
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
        if (isHit)
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

        //need to create waypoint game objects in scene then plug in waypoints of this script
        //otherwise, disable patrol
        if (waypoints.Length > 0)
        {
            Vector3 dir2W = waypoints[currentWaypoint].position - transform.position;
            float dS = 3f * Time.deltaTime;
            Vector3 newPos = transform.position + dir2W.normalized * dS;
            transform.position = newPos;

            distanceToTarget = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
            if (distanceToTarget <= 0.25f) currentWaypoint++;//next target
            if (currentWaypoint > waypoints.Length - 1) currentWaypoint = 0;//restart

            //lookat waypoint without rotating
            transform.LookAt(waypoints[currentWaypoint]);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        }
        
        if (d2P <= AttackDistance)
        {
            canThrow = true;
            ChangeState(States.Attack);
        }
        anim.SetFloat("Speed_f", Mathf.Abs(patrolSpeed));
    }

    void Dead()
    {
        //play dying sound
        source.Pause();
        source.clip = dyingSound;
        source.Play();

        anim.SetBool("isDead", true);
        Invoke("Die", 3);

    }


    void Attack()
    {
        
            if (d2P>AttackDistance)
            {
                ChangeState(States.Patrol);
            }

        //Throw Bird
        if (canThrow)
        {
            canThrow = false;
            Invoke("Throw", throwParrotRate);
        }

        anim.SetFloat("Speed_f", Mathf.Abs(0));
        /*
        //follow waypoints
        distanceToTarget = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if (distanceToTarget <= 0.25f) currentWaypoint++;//next target
        if (currentWaypoint > waypoints.Length - 1) currentWaypoint = 0;//restart


        Vector3 dir2W = waypoints[currentWaypoint].position - transform.position;
        float dS = 2 * Time.deltaTime;
        Vector3 newPos = transform.position + dir2W.normalized * dS;
        transform.position = newPos;
        */


        //lookat player without rotating
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

    }


  

    public void IncrementHits(int hitCount)
    {
        isHit = true;
        hits += hitCount;
        if (hits >= 2) isDead = true;


    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        HUDManager.RemoveHealth(0.1f);
    //        //decrease player health
    //    }
    //}



    void Die()
    {
        GetComponent<CollectibleSpawner>().SpawnCollectible(transform.position);

        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);

    }


    void updateAnim()
    {

        anim.SetBool("hit", isHit);
        anim.SetBool("Throw_b", canThrow);

    }

    private void ExitState()
    {

        isHit = false;
    }
    void Throw()
    {
        if(currentState==States.Attack)
        canThrow = true;
    }

    void ThrowRock()
    {

        //instantiate bird
        // Debug.Log("ThrowBird");
        Instantiate(parrot, rightHand.transform.position, transform.rotation);
    }
}
