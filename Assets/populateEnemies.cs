using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateEnemies : MonoBehaviour
{
    public GameObject goblin;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(goblin, new Vector3(-14, 0.3f, 0), Quaternion.identity);
        Instantiate(goblin, new Vector3(2, 0.3f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
