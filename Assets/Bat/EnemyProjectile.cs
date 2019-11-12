using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        if (other.tag != "Bat")
        {
            Destroy(gameObject);
            if (other.gameObject.tag.Equals("Player"))
            {
                HUDManager.RemoveHealth(0.1f);
            }
        }
    }
}
