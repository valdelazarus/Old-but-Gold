using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFunny : MonoBehaviour
{
    private GameObject player;
    public string[] sentences;
    private float initialMass;
    private bool flying;
    public float activeTime;

    DialogManager dialogManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
        initialMass = player.GetComponent<Rigidbody>().mass;
    }

    // Update is called once per frame
    void Update()
    {
        if (flying == true)
        {
            player.GetComponent<PlayerController>().canJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dialogManager.ShowDialog(sentences);
            StartCoroutine("Fly");
        }
    }

    IEnumerator Fly()
    {
        StartFly();
        yield return new WaitForSeconds(8f);
        StopFly();
    }

    void StartFly()
    {
        flying = true;
        player.GetComponent<Rigidbody>().mass = 0;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(player.transform.position.x + 0.2f, player.transform.position.y + 0.5f, player.transform.position.z);
    }
    void StopFly()
    {
        flying = false;
        player.GetComponent<PlayerController>().canJump = true;
        player.GetComponent<Rigidbody>().mass = initialMass;
        player.GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject);
    }
}
