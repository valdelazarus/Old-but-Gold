using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPunch : MonoBehaviour
{
    public float punchDmg; 

    void Start()
    {
       
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HUDManager.RemoveHealth(punchDmg); 
        }
    }
}
