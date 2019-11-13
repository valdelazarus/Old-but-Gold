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

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (coinTotal >= secretAmount)
            {
                sentence[0] = sentences[0];
                sentence[1] = sentences[1];
            }
            else
            {
                sentence[0] = sentences[2];
                sentence[1] = sentences[3];
            }
            //load sentences into Traveler
            dialog.SetDialogueSentences(sentence);

            //enable ancestor ghost with its appearance animation 
            mysteryAnim.SetTrigger("Talk");

            // end of animation, activate dialog - in AncestorDialog script

            //Destroy(gameObject,1f); // destroy this trigger once activated
        }
    }


}
