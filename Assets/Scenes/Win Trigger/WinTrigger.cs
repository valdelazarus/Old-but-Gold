using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void TriggerWin()
    {
        if (HUDManager.saved == HUDManager.numberOfCaptured && GameObject.FindWithTag("Kraken") == null)
        {
            FindObjectOfType<LevelManager>().LoadScene("Game Win Bonus");
        }
        else
        {
            FindObjectOfType<LevelManager>().LoadScene("Game Win");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TriggerWin();
        }
    }
}
