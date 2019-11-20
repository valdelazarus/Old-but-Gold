using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateryDeath : MonoBehaviour
{
    public GameObject player;

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
        drownWater = GameObject.findObjectWithtag("Drown");
        acidWater = GameObject.findObjectWithtag("Acid");
        if (other.gameObject.tag.Equals(drownWater) || other.gameObject.tag.Equals(acidWater))
        {

        }
    }
}
