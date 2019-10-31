using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    GameObject target;
    Transform player;
    Rigidbody rb;
    Vector3 offset;
    public float transitionTime;//speed of follow

    public float speed ;
    Vector3 dir2P;
    Vector3 newPos;
    float dS;


    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(-1, 1, 1.5f);//offset between ghost and target
       
        Transform target; player = GameObject.FindGameObjectWithTag("Player").transform;//find player
        target = GameObject.FindGameObjectWithTag("target").transform;//find target
                                                            //rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
       
         // transform.position = (target.transform.position + offset + (target.position - transform.position).normalized * (speed * Time.deltaTime));
        //  transform.rotation = (target.transform.rotation);
         transform.LookAt(player);//always face the target

        dir2P = target.transform.position - transform.position;
        dS = 1 * Time.deltaTime;
        newPos = (transform.position) + dir2P.normalized * dS;
        transform.position = newPos;

    }
}
