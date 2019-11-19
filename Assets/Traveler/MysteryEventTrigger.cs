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
    void Start()
    {
        mysteryAnim = GetComponentInChildren<Animator>();
        dialog = GetComponentInChildren<MysteryDialog>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
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

                    //unlock secret path here or call void to unlock it
                }
                //load sentences into Traveler
                dialog.SetDialogueSentences(sentence);


                mysteryAnim.SetBool("Talk", true);
          //  }
        }
    }


}
