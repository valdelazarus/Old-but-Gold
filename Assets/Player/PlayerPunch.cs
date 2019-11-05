using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    private GoblinBehaviour goblin;
    private PirateBehaviour pirate;


    private void OnTriggerEnter(Collider other)
    {
        
        if ( other.gameObject.tag == "Goblin" &&PlayerController.isPunching) 
        {
            goblin = other.gameObject.GetComponent<GoblinBehaviour>();
            goblin.IncrementHits(3);
        }
        else if (other.gameObject.tag == "Pirate" && PlayerController.isPunching)
        {
            pirate = other.gameObject.GetComponent<PirateBehaviour>();
            pirate.IncrementHits(1);
        }
    }
}
