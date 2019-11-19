using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHazard : MonoBehaviour
{
    public float power = 500.0f;
    public float radius = 2.0f;
    public float upForce = 1.0f;
    public ParticleSystem explosionParticles;

    // Start is called before the first frame update
    void Update()
    {
        
    }

    void Start()
    {
        Invoke("Detonate", 5);
    }

    void Detonate()
    {
        Instantiate(explosionParticles, transform.localPosition, transform.localRotation);

        Vector3 explosionPosition = transform.localPosition;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.tag.Equals("Player")) {
                
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
                    HUDManager.RemoveHealth(0.1f);
                }
            }
        }
        Destroy(gameObject);
    }
}
