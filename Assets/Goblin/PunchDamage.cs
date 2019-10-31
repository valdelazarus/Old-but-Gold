using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GoblinBehaviour.isPunching)
        {
            // Debug.Log("HealthDown");
            HUDManager.RemoveHealth(0.1f);
            //decrease player health
        }
    }
}
