using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    private enum States { Patrol, Attack, Dead };
    States currentState = States.Patrol;

    Transform player;

    public float movingDistance;
    public float attackDistance;
    private float startX;
    private float leftBoundary;
    private float rightBoundary;

    private int hits;
    private int maxHits = 1;

    public float speed = 1.5f;
    private Vector3 dir;

    public bool isDead;

    public GameObject particles;
    private Animator anim;

    public GameObject projectilePrefab;
    public GameObject projectileSpawn;
    public float projectileSpeed = 15.0f;
    private float lastShot;
    private float shootingTime;

    private float dmgOnCollide = 0.1f;

    public AudioClip dyingSound;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        hits = 0;
        startX = transform.position.x;
        leftBoundary = startX - movingDistance;
        rightBoundary = startX + movingDistance;
        dir = Vector3.left;

        lastShot = 2f;
        shootingTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x >= transform.position.x - attackDistance && player.position.x <= transform.position.x + attackDistance)
        {
            ChangeState(States.Attack);
        }
        else
        {
            ChangeState(States.Patrol);
        }

        if (isDead)
        {
            ChangeState(States.Dead);
        }

        FSM();

        lastShot += Time.deltaTime;
    }

    private void ChangeState(States toState)
    {
        currentState = toState;
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
                Die();
                break;
        }
    }

    void Patrol()
    {
        if (transform.position.x <= leftBoundary)
        {
            dir = Vector3.right;
        }
        else if(transform.position.x >= rightBoundary)
        {
            dir = Vector3.left;
        }
        if(dir.x < 0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

        float dS = speed * Time.deltaTime;
        Vector3 newPos = transform.position + dir.normalized * dS;
        transform.position = newPos;
    }

    void Attack()
    {
        Vector3 dir2P = player.position - transform.position;
        if (lastShot >= shootingTime)
        {
            Shoot(dir2P);
            lastShot = 0;
        }

        //lookat player without rotating
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void Die()
    {
        //play dying sound
        source.Pause();
        source.clip = dyingSound;
        source.Play();

        Invoke("SelfDestroy", 2f);
    }

    void Shoot(Vector3 dir2P)
    {
        //Instantiate(projectile);
        GameObject p = (GameObject)Instantiate(projectilePrefab, projectileSpawn.transform.position, transform.rotation);
        Rigidbody rb = p.GetComponent<Rigidbody>();
        rb.velocity = dir2P * projectileSpeed;
        Destroy(p, 5); // Destroy rock after n seconds
    }

    public void IncrementHits(int hitCount)
    {
        //isHit = true;
        hits += hitCount;
        if (hits >= maxHits) isDead = true;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HUDManager.RemoveHealth(dmgOnCollide);
        }
    }

    void SelfDestroy()
    {
        GetComponent<CollectibleSpawner>().SpawnCollectible(transform.position);

        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
