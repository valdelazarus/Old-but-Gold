using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryEventTrigger : MonoBehaviour
{
    public string[] sentences;//elements 0,1 is not enough coins. elements 2,3 is enough coins
    private string[] sentence=new string [2];
    public static int coinTotal;
    MysteryDialog dialog;
    Animator mysteryAnim;
    public int secretAmount;//the amount of coins needed to unlock the path
    public Transform pathPosition;

    bool talked;

    void Start()
    {
        mysteryAnim = GetComponentInChildren<Animator>();
        dialog = GetComponentInChildren<MysteryDialog>();
    }

    void Update()
    {
        coinTotal = HUDManager.GetCoins();

        if (dialog.dialogDone && talked)
        {
            dialog.dialogDone = false;
            talked = false;
            TeleportPlayerToSecretPath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            //display "Use E"
            // if (Input.GetKeyDown(KeyCode.E))//removed until interaction text implemented
            // {
            if (coinTotal < secretAmount)
            {

                Debug.Log("sadSentence");
                sentence[0] = sentences[0];
                sentence[1] = sentences[1];
            }
            else
            {
                Debug.Log("happySentence");
                sentence[0] = sentences[2];
                sentence[1] = sentences[3];
            }

            Talk();
            //  }
        }
    }

    private void Talk()
    {
        //load sentences into Traveler
        dialog.SetDialogueSentences(sentence);


        mysteryAnim.SetBool("Talk", true);

        talked = true;
    }

    //calling this when end of dialog
    void TeleportPlayerToSecretPath()
    {
        HUDManager.UseCoins(secretAmount);
        if (pathPosition != null)
        {
            GameObject.FindWithTag("Player").transform.position = pathPosition.position;
        }
    }

}
