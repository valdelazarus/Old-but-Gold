using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCapsule : MonoBehaviour
{
    public float healthAddAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //play power up sound
            FindObjectOfType<PlayerSFX>().PlayPowerUp();

            HUDManager.AddHealth(healthAddAmount);
            Destroy(gameObject);
        }
    }
}
