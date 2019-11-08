using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncestorDialog : MonoBehaviour
{
    string[] sentences; //set in code only

    DialogManager dialogManager;
    bool dialogTriggered;
    Animator anim;

    float initialSpeed;

    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        anim = GetComponent<Animator>();
        initialSpeed = FindObjectOfType<PlayerController>().speed;
    }
    
    private void Update()
    {
        if (dialogTriggered && !dialogManager.dialogPanel.activeInHierarchy)
        {
            //make ancestor disappear once done with dialogs
            anim.SetTrigger("Disappear");
        }
    }
    public void SetDialogueSentences(string[] sentences)
    {
        this.sentences = sentences;
    }

    //animation event - activate dialog at the end of appearing animation
    public void TriggerDialog()
    {
        SetPlayerSpeed(0);

        dialogManager.ShowDialog(sentences);
        dialogTriggered = true;
    }


    //animation event to destroy this object
    public void SelfDestroy()
    {
        
        SetPlayerSpeed(initialSpeed);

        //destroy both parent (the trigger) and this object
        Destroy(GetComponentInParent<AncestorEventTrigger>().gameObject);
    }

    void SetPlayerSpeed(float speed)
    {
        FindObjectOfType<PlayerController>().speed = speed;
    }
}
