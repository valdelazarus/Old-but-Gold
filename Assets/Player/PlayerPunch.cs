using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    private GameObject player;
    bool isPunching;

    private int punchStrength;

    public ParticleSystem wood;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //isPunching = false;
        //GetComponent<BoxCollider>().enabled = false;
        punchStrength = player.GetComponent<PlayerController>().punchStrength;
    }

    private void Update()
    {
        //isPunching = player.GetComponent<PlayerController>().isPunching;
        //if(isPunching)
        //    GetComponent<BoxCollider>().enabled = true;
        punchStrength = player.GetComponent<PlayerController>().punchStrength;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goblin")
        {
            Debug.Log("PUNCHE");
            other.gameObject.GetComponent<GoblinBehaviour>().IncrementHits(punchStrength);
            //GetComponent<BoxCollider>().enabled = false;
        } else if (other.tag == "Destructible")
        {
            other.gameObject.GetComponent<DestructibleSpawn>().spawnItem();
            //CreateWood();
            Destroy(other.gameObject);
            //GetComponent<BoxCollider>().enabled = false;
            
        }
        else if (other.gameObject.tag.Equals("Pirate"))
        {

            other.gameObject.GetComponent<PirateBehaviour>().IncrementHits(punchStrength);
        }
        else if (other.gameObject.tag.Equals("Parrot"))
        {
            other.gameObject.GetComponent<ParrotBehaviour>().IncrementHits(punchStrength);
        }
        else if (other.gameObject.tag.Equals("Bat"))
        {
            other.gameObject.GetComponent<BatBehaviour>().IncrementHits(punchStrength);
        }
        else if (other.gameObject.tag.Equals("Kraken"))
        {
            other.gameObject.GetComponent<KrakenBehaviour>().IncrementHits(punchStrength);
        }
    }

    // Wood Particle System Effect --> needs to be called in the punching and throwing animation
    void CreateWood()
    {
        wood.Play();
    }
}
