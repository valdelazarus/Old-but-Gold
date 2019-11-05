using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectible;

    void Start()
    {
        
    }

    public void SpawnCollectible(Vector3 position)
    {
        Instantiate(collectible, position + 2* Vector3.up, Quaternion.identity);
    }
}
