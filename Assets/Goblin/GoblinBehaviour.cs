using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehaviour : MonoBehaviour
{
    private enum States { Patrol, Chase, Attack, Dead };
    States currentState = States.Patrol;
    Transform player;
    private int punchType;
    public static int hits;
    private float ChaseDistance = 5;
    private float AttackDistance = 3; 
    private float patrolSpeed = 0.25f; 
    private float chasingSpeed = 0.5f;
    private float d2P;
    private bool isPlayerHidden = false;
    private bool canPunch = true;
    private bool isQuitting = false;
    public static bool isDead;
    public static bool isPunching;
    public GameObject particles;
    public static Animator anim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        hits=0;
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
        d2P = Vector3.Distance(transform.position, player.position);

        if (isDead)
        {
            ChangeState(States.Dead);
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
        if (d2P <= ChaseDistance)
        {
            ChangeState(States.Chase);
        }
        anim.SetFloat("Speed_f", Mathf.Abs(patrolSpeed));
    }

    void Dead()
    {
        
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
        
        //move towards player
        Vector3 dir2P = player.position - transform.position;
        float dS = chasingSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + dir2P.normalized * dS;
        transform.position = newPos;

        //lookat player without rotating
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        anim.SetFloat("Speed_f", Mathf.Abs(chasingSpeed));

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

   public static void IncrementHits(int hitCount)
    {
        hits += hitCount;
        if (hits == 3) isDead = true;

        if (!isDead)
        {
            anim.SetBool("hit", true);
        }
    }

    /* Should touching the enemy cause damage?? If so, remove PunchDamage script and uncomment this void.
    private void OnCollisionEnter(Collision collision)
    {
        
            if (collision.gameObject.tag=="Player")
            {
           // Debug.Log("HealthDown");
            HUDManager.RemoveHealth(0.1f);
            //decrease player health
            }


    }*/

   void Die()
    {
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);

    }


}
