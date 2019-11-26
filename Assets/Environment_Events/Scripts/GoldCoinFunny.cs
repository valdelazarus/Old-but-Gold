using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinFunny : MonoBehaviour
{
    private int numOfPickupAttempts = 0;
    public int distanceToMove;
    DialogManager dialogManager;
    public string[] firstAttemptSentences;
    public string[] secondAttemptSentences;
    public string[] thirdAttemptSentences;
    // Start is called before the first frame update
    void Start()
    {
        dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
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
            numOfPickupAttempts++;
            switch(numOfPickupAttempts)
            {
                case 1:
                    dialogManager.ShowDialog(firstAttemptSentences);
                    transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                    break;
                case 2:
                    dialogManager.ShowDialog(secondAttemptSentences);
                    transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                    break;
                case 3:
                    dialogManager.ShowDialog(thirdAttemptSentences);
                    FindObjectOfType<PlayerSFX>().PlayCoin();
                    HUDManager.AddCoins(1);
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
