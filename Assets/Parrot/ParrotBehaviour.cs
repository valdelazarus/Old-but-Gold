using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotBehaviour : MonoBehaviour
{
    Transform player;
    private float playerHeight = 2f;
    private int hits;
    private int maxHits=1;
    public float chasingSpeed = 1.5f;
    private float d2P;
    private bool isQuitting = false;
    public bool isDead;
    public GameObject particles;
    public Animator anim;
    private float birdLifetime = 5;
    float dmgOnCollide = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die",birdLifetime);//destroy bird after it's lifetime
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        hits = 0;
    }
    void Die()
    {

        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        //move towards player
        Vector3 dir2P = player.position - transform.position + new Vector3(0, playerHeight, 0); ;
        float dS = chasingSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + dir2P.normalized * dS;
        transform.position = newPos;

        //lookat player without rotating
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        anim.SetFloat("Speed_f", Mathf.Abs(chasingSpeed));

        if (isDead)
        {
            Die();
        }
    }
    public void IncrementHits(int hitCount)
    {
        //isHit = true;
        hits += hitCount;
        if (hits >= maxHits) isDead = true;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HUDManager.RemoveHealth(dmgOnCollide);
        }
    }
}
