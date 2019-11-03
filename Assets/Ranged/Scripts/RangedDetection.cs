using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDetection : MonoBehaviour
{
    public float rockLifeTime; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //on colliding destroy rocks after its life time
        Destroy(gameObject, rockLifeTime);

        if (other.gameObject.tag.Equals("Enemy"))
        //if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Goblin"))//goblin should play death animation
        {
            GoblinBehaviour.IncrementHits(1);//2 hits to kill goblin
        }

        //need to check for hits in goblin/ specific enemy instead - as following line disables above behaviors (eg. no longer destroy enemy on hit)
        //GetComponent<BoxCollider>().enabled = false; //Removing hit collider so it only hits target once.
    }
}
