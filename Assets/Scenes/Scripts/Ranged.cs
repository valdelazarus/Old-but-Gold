using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : MonoBehaviour
{
    private Vector3 target;
    public GameObject player;
    public GameObject crosshairs;
    //public GameObject rockPrefab;
    //public GameObject rockSpawn;
    //public float rockSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Updating the position of the mouse
        var zPos = Input.mousePosition;
        zPos.z = 7;
        target = transform.GetComponent<Camera>().ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPos.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);

        /* This commented out code makes the player flip around by using the cursor and either move closer/further away from the camera
         * Seems to only work for 2D
        //Vector3 difference = target - player.transform.position;
        //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        */

        /* The commented out code below has the Camera shoot out the rocks -- DO NOT REMOVE
        // Detect when Mouse 0 is clicked -- Left mouse button
        if (Input.GetMouseButtonDown(1))
        {
            // Throw rock
            ThrowRock();
            // Play Throwing Rock Animation

            //float distance = difference.magnitude;
            //Vector2 direction = difference / distance;
            //direction.Normalize();
            //throwRock(direction, rotationZ);
        }
        */
    }

    /* The problem is that the Old Man does not have a HAND JOINT, so the rocks just instantiates at the bottom of his feet!
     * The commented out code below has the instantiates the rocks -- DO NOT REMOVE
    // Creates a clone of the rock prefab
    void ThrowRock()
    {
        GameObject r = (GameObject)Instantiate(rockPrefab, transform.position, transform.rotation);
        Rigidbody rb = r.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * rockSpeed;
        Destroy(r, 10); // Destroy rock after n seconds
    }
    */
}
