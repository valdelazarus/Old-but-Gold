using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smooth;

    Transform target;

    Vector3 offset;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        transform.position = new Vector3(target.position.x, target.position.y + 3.5f, transform.position.z);
        offset = transform.position - target.position;
    }

    
    void Update()
    {

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, target.position.x + offset.x, smooth * Time.deltaTime),
            Mathf.Lerp(transform.position.y, target.position.y + offset.y, smooth * Time.deltaTime),
            transform.position.z);
    }
}
