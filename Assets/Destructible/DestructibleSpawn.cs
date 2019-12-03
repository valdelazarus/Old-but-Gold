using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSpawn: MonoBehaviour
{
    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnRandomItem()
    {
        //GetComponent<BoxCollider>().enabled = false;
        if (items.Length > 0)
        {
            int rand = Random.Range(0, items.Length);
            GameObject item = items[rand];
            Instantiate(item, transform.position + .5f*Vector3.up, Quaternion.identity);
        }
    }
}
