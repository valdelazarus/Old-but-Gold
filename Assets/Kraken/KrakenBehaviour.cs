using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenBehaviour : MonoBehaviour
{
    private Transform player;

    private enum States { Idle, Float, Chase, Fall, Attack, Dead };
    States currentState = States.Idle;

    private Animator anim;
    public GameObject face;

    public int hp = 10;

    private float minHeight;
    private float maxHeight;
    public float speed;

    public GameObject projectilePrefab;
    public GameObject projectileSpawn;
    public float projectileSpeed = 5.0f;

    public float detectRange; 

    private bool isIdle;
    private bool isChasing;
    private bool isSpinning;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        minHeight = this.transform.position.y;
        maxHeight = minHeight + 5f;

        isIdle = false;
        isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();

        FSM();
    }

    void FSM()
    {
        switch (currentState)
        {
            case States.Idle:
                //StartCoroutine(Idle());
                Idle();
                break;
            case States.Float:
                Float();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Attack:
                ChangeState(States.Idle);
                StartCoroutine(Attack());
                break;
            case States.Fall:
                Fall();
                break;
            case States.Dead:
                Die();
                break;
        }
    }

    private void ChangeState(States toState)
    {
        currentState = toState;
    }

    //private IEnumerator Idle()
    //{
    //    if (!isIdle)
    //    {
    //        anim.SetBool("isIdle", true);
    //        isIdle = true;
    //        yield return new WaitForSeconds(10);

    //        isIdle = false;
    //        anim.SetBool("isIdle", false);
    //        ChangeState(States.Float);
    //    }
    //}

    void Idle()
    {
        anim.SetBool("isIdle", true);
        isIdle = true;

        if (Vector3.Distance(transform.position, player.position) <= detectRange)
        {
            isIdle = false;
            anim.SetBool("isIdle", false);
            ChangeState(States.Float);
        }
    }

    private void Float()
    {
        float dS = speed * Time.deltaTime;
        Vector3 newPos = transform.position + Vector3.up * dS;
        transform.position = newPos;

        if (transform.position.y >= maxHeight)
        {
            ChangeState(States.Chase);
        }
    }

    private void Chase()
    {
        if (!isChasing)
        {
            isChasing = true;
            StartCoroutine(Shoot());
            Invoke("startFalling", 5f);
        }
        Vector3 dir2P = player.transform.position - this.transform.position;

        dir2P.Normalize();

        Vector3 newPos = this.transform.position + dir2P * speed * Time.deltaTime;
        this.transform.position = new Vector3(newPos.x, this.transform.position.y, this.transform.position.z);
    }

    private void startFalling()
    {
        isChasing = false;
        ChangeState(States.Fall);
    }

    private void Fall()
    {
        float dS = speed * Time.deltaTime;
        Vector3 newPos = transform.position + Vector3.down * dS;
        transform.position = newPos;

        if (transform.position.y <= minHeight)
        {
            ChangeState(States.Attack);
        }
    }

    private IEnumerator Attack()
    {
        do
        {
            anim.SetTrigger("spin");
            yield return new WaitForSeconds(4);
        } while (isIdle);
    }

    private IEnumerator Shoot()
    {
        do
        {
            Vector3 dir2P = player.position - transform.position;
            //Instantiate(projectile);
            GameObject p = (GameObject)Instantiate(projectilePrefab, projectileSpawn.transform.position, transform.rotation);
            Rigidbody rb = p.GetComponent<Rigidbody>();
            rb.velocity = dir2P * projectileSpeed;
            Destroy(p, 5); // Destroy rock after n seconds


            yield return new WaitForSeconds(2);
        } while (isChasing);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    public void IncrementHits(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            ChangeState(States.Dead);
        }
    }

    public void StartSpinning()
    {
        isSpinning = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    public void StopSpinning()
    {
        isSpinning = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void LookAtPlayer()
    {
        if (!isSpinning)
        {
            if (player.position.x <= this.transform.position.x)
            {
                face.transform.localPosition = new Vector3(2.9f, face.transform.localPosition.y, face.transform.localPosition.z);
                face.transform.eulerAngles = new Vector3(90, 180, -90);
            }
            else
            {
                face.transform.localPosition = new Vector3(-1.3f, face.transform.localPosition.y, face.transform.localPosition.z);
                face.transform.eulerAngles = new Vector3(90, 0, -90);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            HUDManager.RemoveHealth(0.2f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}