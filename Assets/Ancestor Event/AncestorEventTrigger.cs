using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncestorEventTrigger : MonoBehaviour
{
    public string[] sentences;

    AncestorDialog dialog;
    Animator ghostAnim;
    void Start()
    {
        ghostAnim = GetComponentInChildren<Animator>();
        dialog = GetComponentInChildren<AncestorDialog>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //load sentences into ghost
            dialog.SetDialogueSentences(sentences);

            //enable ancestor ghost with its appearance animation 
            ghostAnim.SetTrigger("Appear");

            // end of animation, activate dialog - in AncestorDialog script

            //Destroy(gameObject,1f); // destroy this trigger once activated
        }
    }


}
