using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDetection : MonoBehaviour
{
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
        if (other.gameObject.tag.Equals("Enemy"))
        //if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Goblin"))//goblin should play death animation
        {
            GoblinBehaviour.IncrementHits(1);//2 hits to kill goblin
        }

        GetComponent<BoxCollider>().enabled = false; //Removing hit collider so it only hits target once.
    }
}
