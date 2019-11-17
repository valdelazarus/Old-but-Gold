﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehaviour : MonoBehaviour
{
    private enum States { Patrol, Chase, Attack, Dead };
    States currentState = States.Patrol;
    Transform player;
    private int punchType;
    private  int hits;
    public int maxHits;
    public float ChaseDistance = 5f;
    public float AttackDistance = 3f; 
    public float patrolSpeed = 0.25f; 
    public float chasingSpeed = 0.5f;
    private float d2P;
    private bool isPlayerHidden = false;
    private bool canPunch = true;
    private bool isQuitting = false;
    public  bool isDead;
    public  bool isPunching;
    public  bool isHit;
    public GameObject particles;
    public Animator anim;

    private float distanceToTarget;
    public Transform[] waypoints;
    private int currentWaypoint;

    public float dmgOnCollide;

    public GameObject punchHitBox;

    public AudioClip dyingSound;
    private AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        hits=0;
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
            case States.Chase:
                Chase();
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
            float dS = patrolSpeed * Time.deltaTime;
            Vector3 newPos = transform.position + dir2W.normalized * dS;
            transform.position = newPos;

            if (distanceToTarget <= 0.25f) currentWaypoint++;//next target
            if (currentWaypoint > waypoints.Length - 1) currentWaypoint = 0;//restart
            distanceToTarget = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);

            //lookat waypoint without rotating
            transform.LookAt(waypoints[currentWaypoint]);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        if (d2P <= ChaseDistance)
        {
            ChangeState(States.Chase);
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

    void Chase()
    {    
        if (d2P <= AttackDistance)
        {
            ChangeState(States.Attack);
        }
        else if (d2P > ChaseDistance)
        {
            ChangeState(States.Patrol);
        }
        Vector3 dir2P = player.position - transform.position;
        float dS = chasingSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + dir2P.normalized * dS;
        transform.position = newPos;

        //lookat player without rotating
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        anim.SetFloat("Speed_f", Mathf.Abs(chasingSpeed));

    }

    void Attack()
    {
        if (d2P > AttackDistance && d2P <= ChaseDistance)
        {
            ChangeState(States.Chase);
        }
        else
        {
            //is player hidden
            CheckIfPlayerIsHidden();
            if (isPlayerHidden)
            {
                ChangeState(States.Patrol);
            }
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

        anim.SetFloat("Speed_f", 0);
    }

    

    private void CheckPunch()
    {

        canPunch = true;
    }

    private void CheckIfPlayerIsHidden()
    {
        //throw new NotImplementedException();
        Vector3 E2P = player.position - transform.position;
        E2P.Normalize();
        float cosPhi = Vector3.Dot(E2P, transform.forward);
        isPlayerHidden = (cosPhi < 0);
    }

   public void IncrementHits(int hitCount)
    {
        isHit = true;
        hits += hitCount;
        if (hits >= maxHits) isDead = true;

        
    }


    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.tag=="Player")
            {
                HUDManager.RemoveHealth(dmgOnCollide);
            //decrease player health
            }
    }



    void Die()
    {
        //spawn collectibles
        GetComponent<CollectibleSpawner>().SpawnCollectible(transform.position);
        
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

    public void EnablePunchHitBox()
    {
        punchHitBox.SetActive(true);
    }

    public void DisablePunchHitBox()
    {
        punchHitBox.SetActive(false);
    }
}
