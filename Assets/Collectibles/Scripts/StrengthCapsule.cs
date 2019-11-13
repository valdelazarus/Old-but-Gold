using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthCapsule : MonoBehaviour
{
    public float strengthAdditive;
    public float strengthPowerupTime;
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().addStrength(2, strengthPowerupTime);
            Destroy(gameObject);
        }
    }
}
