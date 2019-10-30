using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCapsuleMovement : MonoBehaviour
{
    private Vector3 startPosition;
    bool up = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
        MoveVertical();
    }

    void MoveVertical()
    {
        var temp = transform.position;
        if (up == true)
        {
            temp.y += 0.003f;
            transform.position = temp;
            if (transform.position.y >= 1.5f)
            {
                up = false;
            }
        }
        if (up == false)
        {
            temp.y -= 0.003f;
            transform.position = temp;
            if (transform.position.y <= 1f)
            {
                up = true;
            }
        }
    }
}
