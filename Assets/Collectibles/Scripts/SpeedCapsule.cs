using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCapsule : MonoBehaviour
{
    public float speedAdditive;
    public float speedPowerupTime;
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().addSpeed(2, speedPowerupTime);
            Destroy(gameObject);
        }
    }
}
