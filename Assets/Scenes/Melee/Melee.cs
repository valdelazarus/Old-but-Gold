using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public GameObject hitBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Punch()
    {
        GameObject cloneHitBox = Instantiate(hitBox, transform.parent) as GameObject;
        Destroy(cloneHitBox, 1f);
        cloneHitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Punch();
        }
    }
}
