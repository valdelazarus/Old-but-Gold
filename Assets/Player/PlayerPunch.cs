using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    private GameObject player;
    bool isPunching;

    private int punchStrength; 

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
            Destroy(other.gameObject);
            //GetComponent<BoxCollider>().enabled = false;
        }
    }
}
