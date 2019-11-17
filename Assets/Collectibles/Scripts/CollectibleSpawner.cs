using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectibles;

    bool spawned; 

    void Start()
    {
        
    }

    public void SpawnCollectible(Vector3 position)
    {
        if (!spawned)
        {
            spawned = true;
            int random = Random.Range(0, collectibles.Length);
            Instantiate(collectibles[random], position + 1f * Vector3.up, Quaternion.identity);
        }
    }
}
