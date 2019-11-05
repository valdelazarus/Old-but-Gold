﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "Goblin") 
        {
            
            GoblinBehaviour.IncrementHits(3);
        }
    }
}
