using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawnFunny : MonoBehaviour
{
    public GameObject crate;
    private Transform player;
    public string[] sentences;

    DialogManager dialogManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dialogManager.ShowDialog(sentences);
            Instantiate(crate, new Vector3(player.position.x + 5, player.position.y, player.position.z), Quaternion.identity);
            Instantiate(crate, new Vector3(player.position.x + 5, player.position.y, player.position.z + 3), Quaternion.identity);
            Instantiate(crate, new Vector3(player.position.x + 5, player.position.y, player.position.z - 3), Quaternion.identity);
            //Instantiate(crate, new Vector3(player.position.x + 5, player.position.y + 3, player.position.z), Quaternion.identity);
            //Instantiate(crate, new Vector3(player.position.x + 5, player.position.y + 3, player.position.z -3), Quaternion.identity);
            //Instantiate(crate, new Vector3(player.position.x + 5, player.position.y + 3, player.position.z + 3), Quaternion.identity);

            Instantiate(crate, new Vector3(player.position.x - 5, player.position.y, player.position.z), Quaternion.identity);
            Instantiate(crate, new Vector3(player.position.x - 5, player.position.y, player.position.z + 3), Quaternion.identity);
            Instantiate(crate, new Vector3(player.position.x - 5, player.position.y, player.position.z - 3), Quaternion.identity);
            //Instantiate(crate, new Vector3(player.position.x - 5, player.position.y + 3, player.position.z), Quaternion.identity);
            //Instantiate(crate, new Vector3(player.position.x - 5, player.position.y + 3, player.position.z - 3), Quaternion.identity);
            //Instantiate(crate, new Vector3(player.position.x - 5, player.position.y + 3, player.position.z + 3), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
