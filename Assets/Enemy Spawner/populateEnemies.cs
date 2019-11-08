using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateEnemies : MonoBehaviour
{
    public Transform[] goblinSpawnPoints;//add spawn points as empty game objects in editor
    public GameObject goblin;

    /*
    public Transform[] pirateSpawnPoints;//add spawn points as empty game objects in editor
    public GameObject pirate;*/
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform goblinSpawnPoint in goblinSpawnPoints)
        {
            Instantiate(goblin, goblinSpawnPoint.position, Quaternion.identity);
        }
        /*Uncomment when ready for pirates
        foreach (Transform pirateSpawnPoint in pirateSpawnPoints)
        {
            Instantiate(goblin, pirateSpawnPoint.position, Quaternion.identity);
        }
        */
    }
}
