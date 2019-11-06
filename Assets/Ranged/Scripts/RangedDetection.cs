using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDetection : MonoBehaviour
{
    public float rockLifeTime;

    public int rockStrength;

    private GoblinBehaviour goblin;
    private PirateBehaviour pirate;
    private ParrotBehaviour parrot;
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

        if (other.gameObject.tag.Equals("Destructible"))
        //if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Goblin"))//goblin should play death animation
        {
            goblin = other.gameObject.GetComponent<GoblinBehaviour>();
            goblin.IncrementHits(rockStrength);//2 hits to kill goblin
            GetComponent<BoxCollider>().enabled = false; //Removing hit collider so it only hits target once.
        }
        else if (other.gameObject.tag.Equals("Pirate"))
        {
            pirate = other.gameObject.GetComponent<PirateBehaviour>();
            pirate.IncrementHits(rockStrength);//3 hits to kill pirate
            GetComponent<BoxCollider>().enabled = false; 
        }
        else if (other.gameObject.tag.Equals("Parrot"))
        {
            parrot = other.gameObject.GetComponent<ParrotBehaviour>();
            parrot.IncrementHits(rockStrength);//3 hits to kill pirate
            GetComponent<BoxCollider>().enabled = false;
        }

        //need to check for hits in goblin/ specific enemy instead - as following line disables above behaviors (eg. no longer destroy enemy on hit)
        //GetComponent<BoxCollider>().enabled = false; //Removing hit collider so it only hits target once.
    }
}
