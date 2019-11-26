using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenProjectile : MonoBehaviour
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
        if (other.tag != "Kraken")
        {
            Destroy(gameObject);
            if (other.gameObject.tag.Equals("Player"))
            {
                HUDManager.RemoveHealth(0.2f);
            }
        }
    }
}
