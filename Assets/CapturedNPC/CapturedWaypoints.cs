using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturedWaypoints : MonoBehaviour
{
    Transform player;

    public GameObject particles;
    private float distanceToTarget;
    public Transform[] waypoints;
    private int currentWaypoint;
    public float patrolSpeed;
    private Vector3 particleRotation = new Vector3(0, 0, 0);
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

  


    //States
    void Patrol()
    {

        //follow waypoints

        //need to create waypoint game objects in scene then plug in waypoints of this script
        //otherwise, npc will stand still
        if (waypoints.Length > 0)
        {
            //animate legs
            anim.SetBool("canRun", true);
            Vector3 dir2W = waypoints[currentWaypoint].position - transform.position;
            float dS = patrolSpeed * Time.deltaTime;
            Vector3 newPos = transform.position + dir2W.normalized * dS;
            transform.position = newPos;

            if (distanceToTarget <= 1f)
            {
                currentWaypoint++;//next target
                Debug.Log("Next");
            }
            if (currentWaypoint > waypoints.Length - 1) currentWaypoint = 0;//restart
            distanceToTarget = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);

            //lookat waypoint without rotating
            transform.LookAt(waypoints[currentWaypoint]);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else
        {
            //dont animate legs
            anim.SetBool("canRun", false);
        }

    }

   


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
