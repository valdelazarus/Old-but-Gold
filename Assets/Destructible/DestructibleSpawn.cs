using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSpawn: MonoBehaviour
{
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnItem()
    {
        GetComponent<BoxCollider>().enabled = false;
        if (item)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
    }
}
