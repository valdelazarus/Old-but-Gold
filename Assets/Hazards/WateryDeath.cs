using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateryDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Would like to have a splash of water animation. Same for the acid.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Died a watery death!");
            //Destroy(other.gameObject);
            FindObjectOfType<LevelManager>().LoadScene("Game Over");
        }
    }
}
